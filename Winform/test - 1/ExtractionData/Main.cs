using SQLite;
using System;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace ExtractionData
{
    public partial class Main : Form
    {

        //protected AsyncLocal<SQLiteAsyncConnection> Db = new AsyncLocal<SQLiteAsyncConnection>();

        public Main()
        {
            InitializeComponent();

            //var databasePath = Path.Combine(Application.StartupPath, "MyData.db");
            //Db.Value = new SQLiteAsyncConnection(databasePath);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void Btn_SelectFile_Click(object sender, EventArgs e)
        {
            //OpenFileDialog dialog = new OpenFileDialog();
            //dialog.Multiselect = false;
            //dialog.Title = @"请选择文件夹";
            //// 小写
            //dialog.Filter = @"文本文件(*.dat)|*.dat|所有文件(*.*)|*.*";

            //if (dialog.ShowDialog() == DialogResult.OK)
            //{
            //    string file = dialog.FileName;
            //    if (!File.Exists(file))
            //    {
            //        MessageBox.Show(@"请选择 .dat 文件", @"选择文件提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return;
            //    }

            //    string extension = Path.GetExtension(file);
            //    if (extension.ToLower() != ".dat")
            //    {
            //        MessageBox.Show(@"请选择 .dat 文件", @"选择文件提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return;
            //    }

            //    this.SetPropertyThreadSafe(control =>
            //    {
            //        control.Btn_Pause.Enabled = true;
            //        control.Btn_Cancel.Enabled = true;
            //        OutputMessage($"已选取 {file} 文件");
            //    });

            //    //await Task.Delay(3000);
            //    await DdInitAsync();
            //    await ReadFile(file);
            //    await CreateExcelAsync();
            //}
            //await ReadFile("D:\\test.DAT");
            //await CreateExcelAsync();

            this.SetPropertyThreadSafe(control =>
            {
                control.Btn_Pause.Enabled = true;
                control.Btn_Cancel.Enabled = true;
            });
            await PauseTest();
        }


        private async Task CreateExcelAsync()
        {
            try
            {
                var dbContext = new FileDataTestDbContext();
                var list = await dbContext.FileDataTest.ToListAsync();

                var fdafds = new List<ExcelData>();
                foreach (var row in list)
                {
                    var ed = new ExcelData();
                    ed.Id = row.Id;

                    //var aa = Convert.FromBase64String("ACF8C7ADC7C0D54F71809B5441EF8DD0");
                    ed.Code = Encrypt.DESEncrypt(row.Data, "ACF8C7ADC7C0D54F71809B5441EF8DD0");
                    ed.Md5 = Encrypt.GetMd5FromString(row.Data);

                    fdafds.Add(ed);
                }

                ExcelExport.Export(fdafds);
            }
            catch (Exception e)
            {

            }
        }

        private async Task DdInitAsync()
        {
            var dbContext = new FileDataTestDbContext();
            OutputMessage($"创建数据库...");
            await dbContext.Database.MigrateAsync();
        }

        private async Task ReadFile(string file)
        {
            StreamReader reader = new StreamReader(file, Encoding.UTF8);
            int index = 0;
            int total = 0;
            string str = string.Empty;

            var dbContext = new FileDataTestDbContext();
            await dbContext.Database.BeginTransactionAsync(IsolationLevel.ReadUncommitted);
            try
            {
                while (!reader.EndOfStream)
                {
                    str = await reader.ReadLineAsync();
                    if (index == 0 && str == null)
                    {
                        OutputMessage($"无法获取记录数量，文件读取终止");
                        break;
                    }

                    if (str == null)
                    {
                        OutputMessage($"当前行数 {index} ，无法获取到数据记录，跳过此行");
                        index++;
                        continue;
                    }

                    // 第一行为 记录数量
                    if (index == 0)
                    {
                        var numberStr = Regex.Replace(str, @"\s", "");
                        if (int.TryParse(numberStr, out int count))
                        {
                            total = count;
                            index++;
                            continue;
                        }

                        OutputMessage($"获取记录数量：{total}");
                    }

                    var result = ConvertAndCheck(str);
                    if (!result.Successed)
                    {
                        Error(result.FailedMessage);
                        break;
                    }

                    await dbContext.FileDataTest.AddAsync(result.FileData);
                    if (index == 100)
                    {
                        await dbContext.SaveChangesAsync();
                    }
                    //await AddAsync(result.FileData);
                    OutputMessage(str);

                    //await Task.Delay(2000);
                    index++;
                }

                await dbContext.SaveChangesAsync();
                await dbContext.Database.CommitTransactionAsync();
            }
            catch (Exception e)
            {
                await dbContext.Database.RollbackTransactionAsync();
                OutputMessage(e.ToString());
                OutputMessage($"行数：{index},错误数据：" + str);
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
                if (array[2].Length > length) return ConvertResult.Failed($"记录数据长度超出 {length} ");
                if (!Is16Base(array[2])) return ConvertResult.Failed("记录数据不是 16 进制 ");

                return ConvertResult.Success(new FileDataTest()
                {
                    Id = Convert.ToInt32(array[0], 16),
                    Length = Convert.ToInt32(array[1], 16),
                    Data = array[2]
                });
            }

            return ConvertResult.Failed($"数据记录格式出错，数据：{str}");
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

        TaskCompletionSource tcs;
        bool con = true;
        private async void Btn_Pause_Click(object sender, EventArgs e)
        {

            if (Btn_Pause.Text == "继续")
            {
                if (tcs != null) tcs.SetResult();
            }
            else
            {
                tcs = new TaskCompletionSource();
            }
            Btn_Pause.SetPropertyThreadSafe(btn =>
            {
                if (btn.Text == "继续")
                {
                    btn.Text = "暂停";
                }
                else
                {
                    btn.Text = "继续";
                }
            });
        }

        private async Task PauseTest()
        {
            while (con)
            {
                await Task.Delay(1000);
                OutputMessage("test");
                if (tcs != null)
                {
                    await tcs.Task;
                }
            }
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            con = false;
        }

        // 1byte = 2个16进制字符
        // 1byte = 8位2进制   ，byte 范围 0-255 ，255 = 二进制 1111 1111

        // 字符串先转 ASCII码  ,  ASCII吗对应不同的字符，例 65 表示 A
    }
}