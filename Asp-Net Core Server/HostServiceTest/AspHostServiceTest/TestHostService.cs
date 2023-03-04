namespace AspHostServiceTest
{
    public class TestHostService : IHostedService, IDisposable
    {
        private Task _executingTask;
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();
        private readonly ILogger<TestHostService> _logger;
        public TestHostService(ILogger<TestHostService> logger)
        {
            _logger = logger;
        }

        private bool _run = true;
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _executingTask = Task.Run(async () =>
            {
                _logger.LogInformation("TestHostService running");
                cancellationToken.Register(() => { _run = false; });
                while (_run)
                {
                    await Task.Delay(10000, cancellationToken);
                    _logger.LogInformation("doing something.");
                }
            }, _stoppingCts.Token);

            // important
            if (_executingTask.IsCompleted)
            {
                return _executingTask;
            }

            // Otherwise it's running
            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("TestHostService stop");

            try
            {
                _stoppingCts.Cancel();
            }
            finally
            {
                await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite, cancellationToken));
            }
        }

        public void Dispose()
        {
            _stoppingCts.Cancel();
        }
    }
}
