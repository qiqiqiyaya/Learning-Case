using System.Threading.Tasks;

namespace ThreadInteractive
{
    public class SalMainOperation
    {
        private readonly SalEmpCalcTaskScheduler _empCalcTaskScheduler;
        private readonly MainThread _mainThread;
        private readonly EmpCalcStatusNotice _notice;
        private SemaphoreSlim concurrencySemaphore = null!;

        public SalMainOperation(SalEmpCalcTaskScheduler empCalcTaskScheduler, MainThread mainThread, EmpCalcStatusNotice notice)
        {
            _empCalcTaskScheduler = empCalcTaskScheduler;
            _mainThread = mainThread;
            _notice = notice;
            concurrencySemaphore = new SemaphoreSlim(empCalcTaskScheduler.MaximumConcurrencyLevel);
        }

        public async Task Startup()
        {
            Console.WriteLine("薪资引擎启动");
        }

        private async Task Concurrent()
        {
            await Task.Delay(3000);
            Console.WriteLine("薪资计算并发执行");

            _notice.Start();
            for (int i = 0; i < 400; i++)
            {
                var code = "emp" + i;
                _notice.AddId(code);
                concurrencySemaphore.Wait();
                var task = new Task(async () =>
                {
                    var empt = new EmpCalcTask(_mainThread);
                    await empt.ExecuteAsync(code);
                });

                task.ContinueWith(SignalTaskComplete);

                task.Start(_empCalcTaskScheduler);

                //if (_empCalcTaskScheduler.Count == _empCalcTaskScheduler.MaximumConcurrencyLevel)
                //{
                //	await Task.Delay(500);
                //}
            }

            await _notice.Task;
            await End();
        }

        private async Task End()
        {
            await Task.Delay(1000);
            Console.WriteLine("薪资计算结束");
        }

        private void SignalTaskComplete(Task completedTask)
        {
            concurrencySemaphore.Release();
        }
    }
}
