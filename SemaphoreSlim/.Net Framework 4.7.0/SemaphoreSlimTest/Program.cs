using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SemaphoreSlimTest
{
    internal class Program
    {
        static readonly SemaphoreSlim Sm = new SemaphoreSlim(1);
        static int Index = 30;
        static readonly CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();

        static void Main(string[] args)
        {
            var token = CancellationTokenSource.Token;
            Task.Run(async () =>
            {
                await Task.Delay(5000);
                while (!token.IsCancellationRequested)
                {
                    await OutPut("A", token);
                }
            }, CancellationToken.None);


            Task.Run(async () =>
            {
                await Task.Delay(5000);
                while (!token.IsCancellationRequested)
                {
                    await OutPut("B", token);
                }
            });


            Task.Run(async () =>
            {
                await Task.Delay(5000);
                while (!token.IsCancellationRequested)
                {
                    await OutPut("C", token);
                }
            });

            while (true)
            {
                Console.WriteLine("按Q键退出");
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Q)
                {
                    Sm.Dispose();
                    break;
                }
            }
        }

        static async Task OutPut(string name, CancellationToken token)
        {
            await Sm.WaitAsync(token);

            await Task.Delay(500, token);
            int result = Index--;
            Console.WriteLine(name + ": Index - 1 = " + result);
            if (Index == 0)
            {
                CancellationTokenSource.Cancel();
            }
            Sm.Release();
        }
    }
}
