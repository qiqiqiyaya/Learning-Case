// See https://aka.ms/new-console-template for more information
using TaskScheduler;


var scheduler = new QueuedTaskScheduler(1);

//for (int i = 0; i < 10; i++)
//{
//    var task = new Task(async () =>
//    {
//        await Task.Delay(3000);
//        Console.WriteLine($"thread name {Thread.CurrentThread.Name}");
//    });
//    task.Start(scheduler);

//    await Task.Delay(1500);
//}

//var task = new Task(async () =>
//{
//    var guid = Guid.NewGuid().ToString("N");
//    await Task.Delay(1000);
//    Console.WriteLine($"thread name {Thread.CurrentThread.Name} {guid}");
//    await Task.Delay(3000);
//    Console.WriteLine($"thread name {Thread.CurrentThread.Name} {guid}");
//});
//task.Start(scheduler);


await Task.Delay(30000);
scheduler.Dispose();
await Task.Delay(30000);
Console.Read();