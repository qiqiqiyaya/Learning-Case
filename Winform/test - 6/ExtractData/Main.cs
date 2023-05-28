using Extract.Domain;
using Extract.Domain.Share;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ExtractData
{
    public partial class Main : Form
    {
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly PauseTokenSource _pauseTokenSource = new PauseTokenSource();

        private readonly IMediator _mediator;
        private readonly IReadFileService _readFileService;
        private readonly IFileDataExportService _fileDataExportService;
        private readonly IDbInitService _dbInitService;
        public Main(IMediator mediator,
            IReadFileService readFileService, 
            IFileDataExportService fileDataExportService,
            IDbInitService dbInitService)
        {
            _mediator = mediator;
            _readFileService = readFileService;
            _fileDataExportService=fileDataExportService;
            _dbInitService=dbInitService;
            InitializeComponent();
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            SystemInitForm systemInitForm = new SystemInitForm("应用程序正在关闭");
            Thread thread = new Thread(() =>
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
                _pauseTokenSource.Dispose();
                Thread.Sleep(500);
                systemInitForm.SetPropertyThreadSafe(@this =>
                {
                    @this.Close();
                    @this.Dispose();
                });
            });
            thread.Start();
            systemInitForm.ShowDialog();
            Close();
            Dispose();
        }

        #region 拖拽
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        private void Pl_Header_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        #endregion

        private async void Btn_SelectFile_Click(object sender, EventArgs e)
        {
            PauseToken pauseToken = _pauseTokenSource.Token;
            // 取消之后，重置取消操作
            if (_cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = new CancellationTokenSource();
            }
            CancellationToken cancellationToken = _cancellationTokenSource.Token;

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

                await OutputMessage($"已选取 {file} 文件", cancellationToken);
                this.SetPropertyThreadSafe(control =>
                {
                    control.Btn_Pause.Enabled = true;
                    control.Btn_Cancel.Enabled = true;
                    Btn_SelectFile.Enabled = false;
                });

                SubOperation(file, pauseToken, cancellationToken);
            }
        }

        private void SubOperation(string file, PauseToken token, CancellationToken cancellationToken)
        {
            Task.Run(async () =>
            {
                try
                {
                    await _readFileService.ReadFile(file, token, cancellationToken);
                    if (token.IsPaused) await token.WaitAsync();

                    var path = await _fileDataExportService.ExportExcel(Application.StartupPath, token,
                        cancellationToken);
                    await OutputMessage("excel文件：" + path, cancellationToken);
                    await _dbInitService.ClearDataAsync();

                    this.SetPropertyThreadSafe(control =>
                    {
                        control.Btn_Pause.Enabled = false;
                        control.Btn_Cancel.Enabled = false;
                        Btn_SelectFile.Enabled = true;
                    });
                }
                catch (OperationCanceledException)
                {
                    // 忽略异常 ， 不需要 取消token
                    await OutputMessage("任务已取消", default);
                }
                catch (Exception ex)
                {
                    // 输出异常信息
                    await OutputMessage(ex.ToString(), default);
                }
                finally
                {
                    // 暂停操作重置
                    _pauseTokenSource.Reset();
                }
            });
        }

        private async Task OutputMessage(string msg, CancellationToken cancellationToken = default)
        {
            await _mediator.Publish(new OutputMessage(msg), cancellationToken);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            SystemInitForm systemInitForm = new SystemInitForm("应用程序初始化...");
            Thread thread = new Thread(() =>
            {
                RootContainer.BeginNewScope(serviceProvider =>
                {
                    var service = serviceProvider.GetRequiredService<IDbInitService>();

                    service.ClearDataAsync().Wait();
                    service.InitAsync(_cancellationTokenSource.Token).Wait();
                    this.SetPropertyThreadSafe(@this =>
                        {
                            @this.Btn_Close.Enabled = true;
                            @this.Btn_SelectFile.Enabled = true;
                        });

                    systemInitForm.SetPropertyThreadSafe(@this =>
                    {
                        @this.Close();
                        @this.Dispose();
                    });
                });
            });
            thread.Start();
            systemInitForm.ShowDialog();
        }

        private async void Btn_Pause_Click(object sender, EventArgs e)
        {
            if (Btn_Pause.Text == @"继续")
            {
                if (_pauseTokenSource.IsPaused)
                {
                    _pauseTokenSource.Resume();
                    await OutputMessage("任务继续...", _cancellationTokenSource.Token);
                    PauseBtnTextChange();
                }
            }
            else
            {
                if (!_pauseTokenSource.IsPaused)
                {
                    await _pauseTokenSource.PauseAsync();
                    await OutputMessage("任务暂停...", _cancellationTokenSource.Token);
                    PauseBtnTextChange();
                }
            }
        }

        private void PauseBtnTextChange()
        {
            Btn_Pause.SetPropertyThreadSafe(btn => { btn.Text = btn.Text == @"继续" ? "暂停" : "继续"; });
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            if (_pauseTokenSource.IsPaused)
            {
                _pauseTokenSource.Resume();
                PauseBtnTextChange();
            }
            _cancellationTokenSource.Cancel();

            this.SetPropertyThreadSafe(@this =>
            {
                @this.Btn_SelectFile.Enabled = true;
                @this.Btn_Pause.Enabled = false;
                @this.Btn_Cancel.Enabled = false;
            });
        }
    }
}