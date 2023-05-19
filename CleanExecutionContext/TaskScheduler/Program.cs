// See https://aka.ms/new-console-template for more information
using System.Threading.Tasks;
using TaskScheduler;

// 1.主线程设置值后，在主线程中开子级线程，值则会传递到子级线程
//AsyncLocalTest.Lang.Value = "test";
//Thread th = new Thread(() =>
//{
//    Console.WriteLine(AsyncLocalTest.Lang.Value);
//});
//th.Start();

//Console.WriteLine(AsyncLocalTest.Lang.Value);
//Console.Read();


// 1.子级线程设置值后，AsyncLocal 不会传递到 th 线程中， the 与 th 线程没有产生过交互
//Thread the = new Thread(() =>
//{
//    AsyncLocalTest.Lang.Value = "test";
//});
//Thread th = new Thread(() =>
//{
//    Console.WriteLine("th线程" + AsyncLocalTest.Lang.Value);
//});
//the.Start();
//th.Start();

//Console.WriteLine("主线程" + AsyncLocalTest.Lang.Value);
//await Task.Delay(3000);
//Console.WriteLine("主线程" + AsyncLocalTest.Lang.Value);
//Console.Read();

//3. 子级线程设置值后，AsyncLocal会在子级启动的tas中传递，外部task、线程不受影响
//var scheduler = new QueuedTaskScheduler(1);
//Thread the = new Thread(() =>
//{
//    AsyncLocalTest.Lang.Value = "test";
//    var task = new Task(() =>
//    {
//        Console.WriteLine("Task:" + AsyncLocalTest.Lang.Value);
//    });
//    task.Start(scheduler);
//});
//Thread th = new Thread(() =>
//{
//    Console.WriteLine("th线程:" + AsyncLocalTest.Lang.Value);
//});
//the.Start();
//th.Start();
//Console.WriteLine("主线程" + AsyncLocalTest.Lang.Value);
//await Task.Delay(3000);
//var task1 = new Task(() =>
//{
//    Console.WriteLine("外部Task:" + AsyncLocalTest.Lang.Value);
//});
//task1.Start(scheduler);
//Console.Read();


//3. 子级线程设置值后，AsyncLocal会在子级启动的tas中传递，外部task、线程不受影响
//var scheduler = new QueuedTaskScheduler(1);
//Task task = null;
//Thread the = new Thread(() =>
//{
//    AsyncLocalTest.Lang.Value = "test";

//    var aa = ExecutionContext.Capture();
//    // Task构造函数中，会执行 ExecutionContext.Capture() ，获取 AsyncLocal 变量值
//    task = new Task(() =>
//    {
//        Console.WriteLine("Task:" + AsyncLocalTest.Lang.Value);
//    });
//});
//Thread th = new Thread(() =>
//{
//    Console.WriteLine("th线程:" + AsyncLocalTest.Lang.Value);
//});
//the.Start();
//th.Start();
//Console.WriteLine("主线程" + AsyncLocalTest.Lang.Value);
//await Task.Delay(3000);
//task.Start(scheduler);
//var task1 = new Task(() =>
//{
//    Console.WriteLine("外部Task:" + AsyncLocalTest.Lang.Value);
//});
//task1.Start(scheduler);
//Console.Read();


//4.thread.start 中会调用 ExecutionContext.Capture()。 可以被阻断
//AsyncLocalTest.Lang.Value = "test";
//Thread the = new Thread((object thread) =>
//{
//    var raa = thread as Thread;
//    var ccc = raa.ExecutionContext;

//    var aa = ExecutionContext.Capture();
//    aa.Dispose();
//    ExecutionContext.Restore(aa);
//    Console.WriteLine("the线程:" + AsyncLocalTest.Lang.Value);
//});
//Thread th = new Thread(() =>
//{
//    Console.WriteLine("th线程:" + AsyncLocalTest.Lang.Value);
//    var aa = ExecutionContext.Capture();
//});
//// 阻断 ExecutionContext 的流动
//ExecutionContext.SuppressFlow();
//// thread.start 中会调用 ExecutionContext.Capture()。
//the.Start(the);
//if (ExecutionContext.IsFlowSuppressed())
//{
//    ExecutionContext.RestoreFlow();
//}
//th.Start();
//Console.WriteLine("主线程" + AsyncLocalTest.Lang.Value);
//Console.Read();


//5. 阻断 ExecutionContext 在 Task 中的流动
//var scheduler = new QueuedTaskScheduler(2);
//AsyncLocalTest.Lang.Value = "test";
//// 阻断 ExecutionContext 的流动
//ExecutionContext.SuppressFlow();
//var task = new Task(() =>
//{
//    var aa = ExecutionContext.Capture();
//    Console.WriteLine("th线程:" + AsyncLocalTest.Lang.Value);
//});
//if (ExecutionContext.IsFlowSuppressed())
//{
//    ExecutionContext.RestoreFlow();
//}
//task.Start(scheduler);
//Console.Read();


//6.创建一个干净的 ExecutionContext 环境，供使用
var scheduler = new QueuedTaskScheduler(2);
AsyncLocalTest.Lang.Value = "test";
using (SuppressExecutionContextFlow.CleanEnvironment())
{
    Task task11 = new Task(() =>
    {
        var aa = ExecutionContext.Capture();
        Console.WriteLine("task11线程:" + AsyncLocalTest.Lang.Value);
    });
    Thread th = new Thread(() =>
    {
        var aa = ExecutionContext.Capture();
        Console.WriteLine("th线程:" + AsyncLocalTest.Lang.Value);
    });
    th.Start();
    task11.Start(scheduler);
}
Console.WriteLine("主线程：" + AsyncLocalTest.Lang.Value);
Console.Read();
