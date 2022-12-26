// See https://aka.ms/new-console-template for more information

using Testing;
using System.Collections.Concurrent;

Console.WriteLine("Hello, World!");


Task.Run(() =>
{
    for (int i = 0; i < 1000; i++)
    {
        var a = new A();
    }
    Console.WriteLine(new A().Concurrent.Count);
});

Task.Run(() =>
{
    for (int i = 0; i < 1000; i++)
    {
        var b = new B();
    }
    Console.WriteLine(new B().Concurrent.Count);
})
.ContinueWith(async (task) =>
{
    await Task.Delay(3000);
    //System.GC.Collect(GC.MaxGeneration);
    var aa = new B().SemaphoreSlim;
#pragma warning disable CS4014
    Task.Run(async () =>
#pragma warning restore CS4014
    {
        while (true)
        {
            await aa.WaitAsync();
            await Task.Delay(3000);
            aa.Release();
        }
    });

    System.GC.Collect();
    Console.WriteLine(new B().Concurrent.Count);

});



//----------------------------------------------------------
Task.Run(() =>
{
    for (int i = 0; i < 1000; i++)
    {
        var a = new ATest();
    }
    Console.WriteLine(new ATest().Concurrent.Count);
});
Task.Run(() =>
    {
        for (int i = 0; i < 1000; i++)
        {
            var a = new BTest();
        }

        Console.WriteLine(new BTest().Concurrent.Count);
    })
    .ContinueWith(async (task) =>
    {
//        await Task.Delay(10000);
//        //System.GC.Collect(GC.MaxGeneration);
//        System.GC.Collect();
//        Console.WriteLine(new BTest().Concurrent.Count);

//        var se = new BTest().SemaphoreSlim;
//        int index = 30;
//        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
//        var token = cancellationTokenSource.Token;

//#pragma warning disable CS4014
//        Task.Run(async () =>
//#pragma warning restore CS4014
//        {
//            await Task.Delay(5000);
//            while (!token.IsCancellationRequested)
//            {
//                await OutPut("A", token);
//            }
//        }, CancellationToken.None);


//#pragma warning disable CS4014
//        Task.Run(async () =>
//#pragma warning restore CS4014
//        {
//            await Task.Delay(5000);
//            while (!token.IsCancellationRequested)
//            {
//                await OutPut("B", token);
//            }
//        });

//#pragma warning disable CS4014
//        Task.Run(async () =>
//#pragma warning restore CS4014
//        {
//            await Task.Delay(5000);
//            while (!token.IsCancellationRequested)
//            {
//                await OutPut("C", token);
//            }
//        }).ContinueWith(task =>
//        {
//            var aa = new BTest().SemaphoreSlim;
//            Task.Run(async () =>
//            {
//                while (true)
//                {
//                    await aa.WaitAsync();
//                    await Task.Delay(3000);
//                    aa.Release();
//                }
//            });
//            System.GC.Collect();

//            if (BTest.WeakReferenceTest.TryGetTarget(out var ccc))
//            {
                
//            }
//            else
//            {
                
//            }

//        });

//        async Task OutPut(string name, CancellationToken token)
//        {
//            await se.WaitAsync(token);

//            await Task.Delay(500, token);
//            int result = index--;
//            Console.WriteLine(name + ": Index - 1 = " + result);
//            if (index == 0)
//            {
//                cancellationTokenSource.Cancel();
//            }

//            se.Release();
//        }
    });

Console.WriteLine("Hello, World!");
Console.ReadLine();


