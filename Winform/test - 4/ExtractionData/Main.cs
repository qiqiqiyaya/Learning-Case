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
        /// 进度
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
                dialog.Title = @"请选择文件夹";
                // 小写
                dialog.Filter = @"文本文件(*.dat)|*.dat|所有文件(*.*)|*.*";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string file = dialog.FileName;
                    if (!File.Exists(file))
                    {
                        MessageBox.Show(@"请选择 .dat 文件", @"选择文件提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    string extension = Path.GetExtension(file);
                    if (extension.ToLower() != ".dat")
                    {
                        MessageBox.Show(@"请选择 .dat 文件", @"选择文件提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    this.SetPropertyThreadSafe(control =>
                    {
                        control.Btn_Pause.Enabled = true;
                        control.Btn_Cancel.Enabled = true;
                        OutputMessage($"已选取 {file} 文件");
                        Btn_SelectFile.Enabled = false;
                    });

                    await SubOperation(file, token, cancellationToken);
                }
            }
            catch (OperationCanceledException)
            {
                // 忽略
                OutputMessage("任务已取消");
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
            OutputMessage("excel文件：" + path);
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
            OutputMessage($"创建数据库...");
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
                    //    OutputMessage($"无法获取记录数量，文件读取终止");
                    //    break;
                    //}
                    
                    if (str == null || string.IsNullOrEmpty(str))
                    {
                        var num = index + 1;
                        OutputMessage($"当前行数 {num} ，无法获取到数据记录，跳过此行");
                        continue;
                    }

                    // 第一行为 记录数量
                    if (readFirstNum)
                    {
                        readFirstNum = false;
                        var numberStr = Regex.Replace(str, @"\s", "");
                        if (int.TryParse(numberStr, out int count))
                        {
                            total = count;
                            OutputMessage($"获取记录数量：{total}");
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
                    OutputMessage($"记录数量：{total} , 但实际上获取了的记录数量：{index}");
                }

                await dbContext.SaveChangesAsync(cancellationToken);
                await dbContext.Database.CommitTransactionAsync(cancellationToken);
            }
            catch (Exception e)
            {
                // 不用取消回滚
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
                //记录标记检查
                if (array[0].Length > 2) return ConvertResult.Failed("记录标记长度超出 2 ");
                if (!Is16Base(array[0])) return ConvertResult.Failed("记录标记不是 16 进制 ");

                if (array[1].Length > 1) return ConvertResult.Failed("记录长度超出 1 ");
                if (!Is16Base(array[1])) return ConvertResult.Failed("记录长度不是 16 进制 ");

                var length = Convert.ToInt32(array[1], 16);
                if (array[2].Length != length) return ConvertResult.Failed($"记录数据长度错误 {length} ，实践数据长度 {array[2].Length} ，跳过此行");
                if (!Is16Base(array[2])) return ConvertResult.Failed("记录数据不是 16 进制 ");

                return ConvertResult.Success(new FileDataTest()
                {
                    Id = Convert.ToInt32(array[0], 16),
                    Length = Convert.ToInt32(array[1], 16),
                    Data = array[2]
                });
            }

            return ConvertResult.Failed($"数据记录格式出错，数据：{str} ，跳过此行");
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
            MessageBox.Show(msg, @"错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 是否是16进制
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
            if (Btn_Pause.Text == "继续")
            {
                if (_pauseTokenSource.IsPaused)
                {
                    _pauseTokenSource.Resume();
                    OutputMessage("继续...");
                    Btn_Pause.SetPropertyThreadSafe(btn => { btn.Text = btn.Text == "继续" ? "暂停" : "继续"; });
                }
            }
            else
            {
                if (!_pauseTokenSource.IsPaused)
                {
                    await _pauseTokenSource.PauseAsync();
                    OutputMessage("暂停...");
                    Btn_Pause.SetPropertyThreadSafe(btn => { btn.Text = btn.Text == "继续" ? "暂停" : "继续"; });
                }
            }
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            if (_pauseTokenSource.IsPaused)
            {
                _pauseTokenSource.Resume();
                Btn_Pause.SetPropertyThreadSafe(btn => { btn.Text = btn.Text == "继续" ? "暂停" : "继续"; });
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

        // 1byte = 2个16进制字符
        // 1byte = 8位2进制   ，byte 范围 0-255 ，255 = 二进制 1111 1111

        // 字符串先转 ASCII码  ,  ASCII吗对应不同的字符，例 65 表示 A
    }
}