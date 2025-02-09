using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace ThreadInteractive
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SalMainTaskScheduler taskScheduler = new SalMainTaskScheduler(1);
            SalEmpCalcTaskScheduler empCalcTaskScheduler = new SalEmpCalcTaskScheduler(5);
            EmpCalcStatusNotice notice = new EmpCalcStatusNotice();
            MainThread mainThread = new MainThread(taskScheduler, notice);

            SalMainOperation mainOperation = new SalMainOperation(empCalcTaskScheduler, mainThread, notice);

            Task engine = new Task(async () =>
            {
                await mainOperation.Startup();
            });


            engine.Start(taskScheduler);

            Console.Read();
        }

    }
}
