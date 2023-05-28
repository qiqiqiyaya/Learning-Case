using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace ExtractionData
{
    public partial class Main : Form
    {
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private PauseTokenSource _pauseTokenSource = new PauseTokenSource();

        //protected AsyncLocal<SQLiteAsyncConnection> Db = new AsyncLocal<SQLiteAsyncConnection>();

        /// <summary>
        /// ����
        /// </summary>
        private IProgress<int> _progress;

        public Main()
        {
            InitializeComponent();

            _progress = new Progress<int>(i => Pb_Progress.Value = i);
            CenterToScreen();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void Btn_SelectFile_Click(object sender, EventArgs e)
        {
            PauseToken token = _pauseTokenSource.Token;
            if (_cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = new CancellationTokenSource();
            }
            CancellationToken cancellationToken = _cancellationTokenSource.Token;

            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Multiselect = false;
                dialog.Title = @"��ѡ���ļ���";
                // Сд
                dialog.Filter = @"�ı��ļ�(*.dat)|*.dat|�����ļ�(*.*)|*.*";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string file = dialog.FileName;
                    if (!File.Exists(file))
                    {
                        MessageBox.Show(@"��ѡ�� .dat �ļ�", @"ѡ���ļ���ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    string extension = Path.GetExtension(file);
                    if (extension.ToLower() != ".dat")
                    {
                        MessageBox.Show(@"��ѡ�� .dat �ļ�", @"ѡ���ļ���ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    this.SetPropertyThreadSafe(control =>
                    {
                        control.Btn_Pause.Enabled = true;
                        control.Btn_Cancel.Enabled = true;
                        OutputMessage($"��ѡȡ {file} �ļ�");
                        Btn_SelectFile.Enabled = false;
                    });

                    await SubOperation(file, token, cancellationToken);
                }
            }
            catch (OperationCanceledException)
            {
                // ����
                OutputMessage("������ȡ��");
            }

            this.SetPropertyThreadSafe(control =>
            {
                control.Btn_Pause.Enabled = false;
                control.Btn_Cancel.Enabled = false;
                Btn_SelectFile.Enabled = true;
            });

        }

        private async Task SubOperation(string file,PauseToken token, CancellationToken cancellationToken)
        {
            await DdInitAsync(cancellationToken);
            if (token.IsPaused) await token.WaitAsync();

            await ReadFile(file, token, cancellationToken);
            if (token.IsPaused) await token.WaitAsync();

            var path = await CreateExcelAsync(token, cancellationToken);
            OutputMessage("excel�ļ���" + path);
        }

        private async Task<string> CreateExcelAsync(PauseToken token, CancellationToken cancellationToken)
        {
            var dbContext = new FileDataTestDbContext();
            var list = await dbContext.FileDataTest.ToListAsync(cancellationToken);

            var data = new List<ExcelData>();
            foreach (var row in list)
            {
                if(cancellationToken.IsCancellationRequested) cancellationToken.ThrowIfCancellationRequested();
                if (token.IsPaused) await token.WaitAsync();

                var ed = new ExcelData();
                ed.Id = row.Id;

                //var aa = Convert.FromBase64String("ACF8C7ADC7C0D54F71809B5441EF8DD0");
                ed.Code = Encrypt.DESEncrypt(row.Data, "ACF8C7ADC7C0D54F71809B5441EF8DD0");
                ed.Md5 = Encrypt.GetMd5FromString(row.Data);

                data.Add(ed);
            }

            return ExcelExport.Export(data);
        }

        private async Task DdInitAsync(CancellationToken cancellationToken)
        {
            var dbContext = new FileDataTestDbContext();
            OutputMessage($"�������ݿ�...");
            await dbContext.Database.MigrateAsync(cancellationToken);
        }

        private async Task ReadFile(string file,PauseToken token,CancellationToken cancellationToken)
        {
            StreamReader reader = new StreamReader(file, Encoding.UTF8);
            int index = 0;
            int total = 0;
            string str = string.Empty;
            bool readFirstNum = true;

            var dbContext = new FileDataTestDbContext();
            await dbContext.Database.BeginTransactionAsync(IsolationLevel.ReadUncommitted, cancellationToken);
            try
            {
                while (!reader.EndOfStream)
                {
                    if(cancellationToken.IsCancellationRequested) cancellationToken.ThrowIfCancellationRequested();

                    str = await reader.ReadLineAsync();
                    await Task.Delay(500);
                    //if (index == 0 && str == null)
                    //{
                    //    OutputMessage($"�޷���ȡ��¼�������ļ���ȡ��ֹ");
                    //    break;
                    //}
                    
                    if (str == null || string.IsNullOrEmpty(str))
                    {
                        var num = index + 1;
                        OutputMessage($"��ǰ���� {num} ���޷���ȡ�����ݼ�¼����������");
                        continue;
                    }

                    // ��һ��Ϊ ��¼����
                    if (readFirstNum)
                    {
                        readFirstNum = false;
                        var numberStr = Regex.Replace(str, @"\s", "");
                        if (int.TryParse(numberStr, out int count))
                        {
                            total = count;
                            OutputMessage($"��ȡ��¼������{total}");
                            continue;
                        }
                    }

                    var result = ConvertAndCheck(str);
                    if (!result.Successed)
                    {
                        OutputMessage(result.FailedMessage);
                        continue;
                    }

                    await dbContext.FileDataTest.AddAsync(result.FileData, cancellationToken);
                    if (index == 100)
                    {
                        await dbContext.SaveChangesAsync(cancellationToken);
                    }
                    OutputMessage(str);

                    index++;
                    if(token.IsPaused) await token.WaitAsync();
                }

                if (index != total)
                {
                    OutputMessage($"��¼������{total} , ��ʵ���ϻ�ȡ�˵ļ�¼������{index}");
                }

                await dbContext.SaveChangesAsync(cancellationToken);
                await dbContext.Database.CommitTransactionAsync(cancellationToken);
            }
            catch (Exception e)
            {
                // ����ȡ���ع�
                await dbContext.Database.RollbackTransactionAsync();
                if (!(e is OperationCanceledException))
                {
                    OutputMessage(e.ToString());
                }
                return;
            }
            finally
            {
                reader.Close();
                reader.Dispose();
            }
        }

        private async Task AddAsync(FileDataTest data)
        {
            var dbContext = new FileDataTestDbContext();
            await dbContext.FileDataTest.AddAsync(data);

            await dbContext.SaveChangesAsync();
        }

        private ConvertResult ConvertAndCheck(string str)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));


            var array = str.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (array.Length >= 2)
            {
                //��¼��Ǽ��
                if (array[0].Length > 2) return ConvertResult.Failed("��¼��ǳ��ȳ��� 2 ");
                if (!Is16Base(array[0])) return ConvertResult.Failed("��¼��ǲ��� 16 ���� ");

                if (array[1].Length > 1) return ConvertResult.Failed("��¼���ȳ��� 1 ");
                if (!Is16Base(array[1])) return ConvertResult.Failed("��¼���Ȳ��� 16 ���� ");

                var length = Convert.ToInt32(array[1], 16);
                if (array[2].Length != length) return ConvertResult.Failed($"��¼���ݳ��ȴ��� {length} ��ʵ�����ݳ��� {array[2].Length} ����������");
                if (!Is16Base(array[2])) return ConvertResult.Failed("��¼���ݲ��� 16 ���� ");

                return ConvertResult.Success(new FileDataTest()
                {
                    Id = Convert.ToInt32(array[0], 16),
                    Length = Convert.ToInt32(array[1], 16),
                    Data = array[2]
                });
            }

            return ConvertResult.Failed($"���ݼ�¼��ʽ�������ݣ�{str} ����������");
        }

        private void OutputMessage(string msg)
        {
            Txt_OutputMsg.SetPropertyThreadSafe(txt =>
            {
                txt.AppendText(msg + "\r\n");
                txt.ScrollToCaret();
            });
        }

        private void SetProgress(int value)
        {
            Pb_Progress.SetPropertyThreadSafe(control =>
            {
                control.Value = value;
            });
        }

        private void Error(string msg)
        {
            MessageBox.Show(msg, @"������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// �Ƿ���16����
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static bool Is16Base(string hex)
        {
            const string PATTERN = @"[A-Fa-f0-9]+$";
            return Regex.IsMatch(hex, PATTERN);
        }

        private async void Btn_Pause_Click(object sender, EventArgs e)
        {
            if (Btn_Pause.Text == "����")
            {
                if (_pauseTokenSource.IsPaused)
                {
                    _pauseTokenSource.Resume();
                    OutputMessage("����...");
                    Btn_Pause.SetPropertyThreadSafe(btn => { btn.Text = btn.Text == "����" ? "��ͣ" : "����"; });
                }
            }
            else
            {
                if (!_pauseTokenSource.IsPaused)
                {
                    await _pauseTokenSource.PauseAsync();
                    OutputMessage("��ͣ...");
                    Btn_Pause.SetPropertyThreadSafe(btn => { btn.Text = btn.Text == "����" ? "��ͣ" : "����"; });
                }
            }
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            if (_pauseTokenSource.IsPaused)
            {
                _pauseTokenSource.Resume();
                Btn_Pause.SetPropertyThreadSafe(btn => { btn.Text = btn.Text == "����" ? "��ͣ" : "����"; });
            }
            _cancellationTokenSource.Cancel();

            this.SetPropertyThreadSafe(@this =>
            {
                @this.Btn_SelectFile.Enabled = true;
                @this.Btn_Pause.Enabled = false;
                @this.Btn_Cancel.Enabled = false;
            });
        }

        private async void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _pauseTokenSource.Dispose();

            await Task.Delay(30000);
        }

        // 1byte = 2��16�����ַ�
        // 1byte = 8λ2����   ��byte ��Χ 0-255 ��255 = ������ 1111 1111

        // �ַ�����ת ASCII��  ,  ASCII���Ӧ��ͬ���ַ����� 65 ��ʾ A
    }
}