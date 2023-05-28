using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace ExtractionData
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void Btn_SelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Title = @"��ѡ���ļ���";
            // Сд
            dialog.Filter = @"�ı��ļ�(*.dat)|*.dat|�����ļ�(*.*)|*.*";

            string file = string.Empty;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                file = dialog.FileName;
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
            }

            this.SetPropertyThreadSafe(control =>
            {
                control.Btn_Pause.Enabled = true;
                control.Btn_Cancel.Enabled = true;
                OutputMessage($"��ѡȡ {file} �ļ�");
            });

            await ReadFile(file);
            //await Task.Run(() =>
            //{
            //    this.SetPropertyThreadSafe(control =>
            //    {
            //        control.Pb_Progress.Value = 50;
            //    });
            //});

            //Pb_Progress.SetPropertyThreadSafe(() => Pb_Progress.Value, 50);
        }

        private async Task ReadFile(string file)
        {
            SetProgress(1);

            StreamReader reader = new StreamReader(file, Encoding.UTF8);
            int index = 0;
            int total = 0;

            while (!reader.EndOfStream)
            {
                var str = await reader.ReadLineAsync();
                if (str == null) continue;
                // ��һ��Ϊ ��¼����
                if (index == 0)
                {
                    var numberStr = Regex.Replace(str, @"\s", "");
                    if (int.TryParse(numberStr, out int count))
                    {
                        total = count;
                    }

                    OutputMessage($"��ȡ��¼������{total}");
                }

                index++;
                OutputMessage(str);
            }

        }

        private void OutputMessage(string msg)
        {
            Txt_OutputMsg.SetPropertyThreadSafe(txt =>
            {
                txt.Text += msg + "\r\n";
            });
        }

        private void SetProgress(int value)
        {
            Pb_Progress.SetPropertyThreadSafe(control =>
            {
                control.Value = value;
            });
        }
    }
}