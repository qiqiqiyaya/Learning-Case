using Extract.Domain;
using Extract.Domain.Share;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

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
        private string _excelPath = string.Empty;
        private readonly ILogger _logger;
        public Main(IMediator mediator,
            IReadFileService readFileService,
            IFileDataExportService fileDataExportService,
            IDbInitService dbInitService,
            ILogger<DbInitService> logger)
        {
            _mediator = mediator;
            _readFileService = readFileService;
            _fileDataExportService = fileDataExportService;
            _dbInitService = dbInitService;
            _logger = logger;
            InitializeComponent();
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            SystemInitForm systemInitForm = new SystemInitForm("Ӧ�ó������ڹر�");
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

        #region ��ק
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
            // ȡ��֮������ȡ������
            if (_cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = new CancellationTokenSource();
            }
            CancellationToken cancellationToken = _cancellationTokenSource.Token;

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

                await OutputMessage($"��ѡȡ {file} �ļ�", cancellationToken);
                this.SetPropertyThreadSafe(control =>
                {
                    control.Btn_Pause.Enabled = true;
                    control.Btn_Cancel.Enabled = true;
                    Btn_SelectFile.Enabled = false;
                });

                _logger.LogInformation("�������ļ��ж�ȡ���ݲ���");
                SubOperation(file, pauseToken, cancellationToken);
            }
        }

        private void SubOperation(string file, PauseToken token, CancellationToken cancellationToken)
        {
            Task.Run(async () =>
            {
                try
                {
                    await _readFileService.ReadFile(file, cancellationToken, token);
                    if (token.IsPaused) await token.WaitAsync();

                    var path = await _fileDataExportService.ExportExcel(Application.StartupPath, token,
                        cancellationToken);
                    _excelPath = path;
                    await OutputMessage("excel�ļ���" + path, cancellationToken);
                    await _dbInitService.ClearDataAsync();
                    _logger.LogInformation("���ļ��ж�ȡ���ݣ�������Excel���");

                    this.SetPropertyThreadSafe(control =>
                    {
                        control.Btn_Pause.Enabled = false;
                        control.Btn_Cancel.Enabled = false;
                        Btn_SelectFile.Enabled = true;
                        Btn_OpenExcel.Enabled = true;
                    });
                }
                catch (OperationCanceledException)
                {
                    // �����쳣 �� ����Ҫ ȡ��token
                    await OutputMessage("������ȡ��", default);
                }
                catch (Exception ex)
                {
                    // ����쳣��Ϣ
                    await OutputMessage(ex.ToString(), default);
                }
                finally
                {
                    // ��ͣ��������
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
            SystemInitForm systemInitForm = new SystemInitForm("Ӧ�ó����ʼ��...");
            Thread thread = new Thread(() =>
            {
                RootContainer.BeginNewScope(serviceProvider =>
                {
                    var service = serviceProvider.GetRequiredService<IDbInitService>();

                    service.InitAsync(_cancellationTokenSource.Token).Wait();
                    service.ClearDataAsync().Wait();
                    _logger.LogInformation("Ӧ�ó����ʼ�����");
                    Thread.Sleep(5000);
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
            if (Btn_Pause.Text == @"����")
            {
                if (_pauseTokenSource.IsPaused)
                {
                    _pauseTokenSource.Resume();
                    await OutputMessage("�������...", _cancellationTokenSource.Token);
                    PauseBtnTextChange();
                    _logger.LogInformation("�������...");
                }
            }
            else
            {
                if (!_pauseTokenSource.IsPaused)
                {
                    await _pauseTokenSource.PauseAsync();
                    await OutputMessage("������ͣ...", _cancellationTokenSource.Token);
                    PauseBtnTextChange();
                    _logger.LogInformation("������ͣ...");
                }
            }
        }

        private void PauseBtnTextChange()
        {
            Btn_Pause.SetPropertyThreadSafe(btn => { btn.Text = btn.Text == @"����" ? "��ͣ" : "����"; });
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            if (_pauseTokenSource.IsPaused)
            {
                _pauseTokenSource.Resume();
                PauseBtnTextChange();
            }
            _cancellationTokenSource.Cancel();
            _logger.LogInformation("����ȡ��...");

            this.SetPropertyThreadSafe(@this =>
            {
                @this.Btn_SelectFile.Enabled = true;
                @this.Btn_Pause.Enabled = false;
                @this.Btn_Cancel.Enabled = false;
                Btn_OpenExcel.Enabled = false;
            });
        }

        private void Btn_OpenExcel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_excelPath)) return;
            using var process = new Process();
            process.StartInfo = new ProcessStartInfo(_excelPath)
            {
                UseShellExecute = true
            };
            process.Start();
            _logger.LogInformation("��Excel,·����" + _excelPath);
        }
    }
}