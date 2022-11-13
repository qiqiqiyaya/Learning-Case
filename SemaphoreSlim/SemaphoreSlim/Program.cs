// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

// SemaphoreTest();

//SemaphoreTest1();

SemaphoreTest2();

Console.ReadKey();

// 现在有10个人要过桥
// 但是一座桥上只能承受5个人，再多桥就会塌
static void SemaphoreTest()
{
    var semaphore = new SemaphoreSlim(5);
    for (int i = 1; i <= 10; i++)
    {
        Thread.Sleep(100); // 排队上桥
        var index = i; // 定义index 避免出现闭包的问题
        Task.Run(() =>
        {
            semaphore.Wait();
            try
            {
                Console.WriteLine($"第{index}个人正在过桥。线程Id  " + Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(5000); // 模拟过桥需要花费的时间
            }
            finally
            {
                Console.WriteLine($"第{index}个人已经过桥。线程Id  " + Thread.CurrentThread.ManagedThreadId);
                semaphore.Release();
            }
        });
    }
}

// 现在有10个人要过桥
// 但是一座桥上只能承受5个人，再多桥就会塌
static void SemaphoreTest1()
{
    var semaphore = new SemaphoreSlim(5);
    for (int i = 1; i <= 10; i++)
    {
        Thread.Sleep(100); // 排队上桥
        var index = i; // 定义index 避免出现闭包的问题
        Task.Run(async () =>
        {
            Console.WriteLine($"第{index}个人已抵达桥边上。线程Id  " + Thread.CurrentThread.ManagedThreadId);
            semaphore.Wait();
            try
            {
                Console.WriteLine($"第{index}个人正在过桥。线程Id  " + Thread.CurrentThread.ManagedThreadId);
                //Thread.Sleep(5000); // 模拟过桥需要花费的时间
                await Task.Delay(5000);
            }
            finally
            {
                Console.WriteLine($"第{index}个人已经过桥。线程Id  " + Thread.CurrentThread.ManagedThreadId);
                semaphore.Release();
            }
        });
    }
}


static void SemaphoreTest2()
{
    var semaphore = new SemaphoreSlim(5);
    for (int i = 1; i <= 10; i++)
    {
        Thread.Sleep(100); // 排队上桥
        var index = i; // 定义index 避免出现闭包的问题
        Task.Run(async () =>
        {
            Console.WriteLine($"第{index}个人已抵达桥边上。线程Id  " + Thread.CurrentThread.ManagedThreadId);
            await semaphore.WaitAsync();
            try
            {
                Console.WriteLine($"第{index}个人正在过桥。线程Id  " + Thread.CurrentThread.ManagedThreadId);
                await Task.Delay(5000);// 模拟过桥需要花费的时间
            }
            finally
            {
                Console.WriteLine($"第{index}个人已经过桥。线程Id  " + Thread.CurrentThread.ManagedThreadId);
                semaphore.Release();
            }
        });
    }
}