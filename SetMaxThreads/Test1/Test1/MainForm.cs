namespace Test1
{
    public partial class Form1 : Form
    {

        private CancellationTokenSource _cts;
        private int _maxWorkerThreads;
        private int _maxIOThreads;

        public Form1()
        {
            InitializeComponent();

            // 根据 CPU 核心数动态配置（建议值参考）
            int processorCount = Environment.ProcessorCount;
            _maxWorkerThreads = processorCount * 2;    // CPU 密集型任务推荐 1.5N~2N
            _maxIOThreads = processorCount * 4;        // I/O 密集型任务推荐 3N~5N

            // 配置线程池（全局设置需谨慎）
            ThreadPool.SetMinThreads(processorCount, processorCount); // 避免初始延迟
            ThreadPool.SetMaxThreads(_maxWorkerThreads, _maxIOThreads);

            // 初始化取消令牌
            _cts = new CancellationTokenSource();
        }

        // 示例：启动 CPU 密集型任务
        private async void btnStartCpuTask_Click(object sender, EventArgs e)
        {
            btnStartCpuTask.Enabled = false;
            try
            {
                await Task.Run(() => ExecuteCpuBoundWork(_cts.Token), _cts.Token);
                UpdateStatus("CPU 任务完成");
            }
            catch (OperationCanceledException)
            {
                UpdateStatus("CPU 任务已取消");
            }
            finally
            {
                btnStartCpuTask.Enabled = true;
            }
        }

        // 示例：启动 I/O 密集型任务
        private async void btnStartIoTask_Click(object sender, EventArgs e)
        {
            btnStartIoTask.Enabled = false;
            try
            {
                await ExecuteIoBoundWorkAsync(_cts.Token);
                UpdateStatus("I/O 任务完成");
            }
            catch (OperationCanceledException)
            {
                UpdateStatus("I/O 任务已取消");
            }
            finally
            {
                btnStartIoTask.Enabled = true;
            }
        }

        // CPU 密集型任务示例
        private void ExecuteCpuBoundWork(CancellationToken ct)
        {
            Parallel.For(0, 100, new ParallelOptions
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount, // 限制并发数
                CancellationToken = ct
            }, i =>
            {
                if (ct.IsCancellationRequested) return;

                // 模拟计算密集型操作
                var result = CalculateFibonacci(30);

                // 跨线程更新 UI
                this.Invoke((Action)(() =>
                    lstResults.Items.Add($"任务 {i}: 结果={result}")));
            });
        }

        // I/O 密集型任务示例（使用真正的异步操作）
        private async Task ExecuteIoBoundWorkAsync(CancellationToken ct)
        {
            var httpClient = new System.Net.Http.HttpClient();
            var tasks = new Task<string>[10];

            for (int i = 0; i < 10; i++)
            {
                tasks[i] = httpClient.GetStringAsync("https://example.com", ct);
            }

            var results = await Task.WhenAll(tasks);

            // 更新 UI
            this.Invoke((Action)(() =>
            {
                lstResults.Items.Clear();
                lstResults.Items.AddRange(results);
            }));
        }

        // 监控线程池状态
        private void btnCheckThreads_Click(object sender, EventArgs e)
        {
            ThreadPool.GetAvailableThreads(out int worker, out int io);
            UpdateStatus($"可用线程 - 工作线程: {worker}, I/O线程: {io}");
        }

        // 安全更新 UI 控件
        private void UpdateStatus(string message)
        {
            if (lblStatus.InvokeRequired)
            {
                this.Invoke((Action)(() => lblStatus.Text = message));
            }
            else
            {
                lblStatus.Text = message;
            }
        }

        // 示例计算方法
        private static long CalculateFibonacci(int n)
        {
            if (n <= 1) return n;
            return CalculateFibonacci(n - 1) + CalculateFibonacci(n - 2);
        }

        // 窗体关闭时释放资源
        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            _cts.Cancel();
            Thread.Sleep(500); // 等待任务取消
            base.OnFormClosing(e);
        }
    }
}
