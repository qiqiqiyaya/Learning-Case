namespace AspHostServiceTest
{
    public class BackgroundServiceTest : BackgroundService
    {
        private readonly ILogger<BackgroundServiceTest> _logger;
        public BackgroundServiceTest(ILogger<BackgroundServiceTest> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("doing something.");
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(10000, stoppingToken);
                _logger.LogInformation("doing something.");
            }
            _logger.LogInformation("TestHostService stop");
        }
    }
}
