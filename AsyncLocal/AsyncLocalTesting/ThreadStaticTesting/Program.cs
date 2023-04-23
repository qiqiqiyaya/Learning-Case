// See https://aka.ms/new-console-template for more information

namespace ThreadStaticTesting;

class Program
{
    [ThreadStatic]
    private static string _threadStatic;
    private static ThreadLocal<string> _threadLocal = new ThreadLocal<string>();
    private static AsyncLocal<string> _asyncLocal = new AsyncLocal<string>();
    static void Main(string[] args)
    {
        _threadStatic = "ThreadStatic保存的数据";
        _threadLocal.Value = "ThreadLocal保存的数据";
        _asyncLocal.Value = "AsyncLocal保存的数据";
        PrintValuesInAnotherThread();
        Console.ReadKey();
    }

    private static void PrintValuesInAnotherThread()
    {
        // Thread change
        Task.Run(() =>
        {
            var aa = Thread.CurrentThread.ExecutionContext;

            Console.WriteLine($"ThreadStatic: {_threadStatic}");
            Console.WriteLine($"ThreadLocal: {_threadLocal.Value}");
            Console.WriteLine($"AsyncLocal: {_asyncLocal.Value}");
        });
    }
}


//class Program
//{
//    [ThreadStatic]
//    private static string _threadStatic;
//    private static ThreadLocal<string> _threadLocal = new ThreadLocal<string>();

//    static void Main(string[] args)
//    {
//        // in same thread
//        Parallel.For(0, 4, _ =>
//        {
//            var threadId = Thread.CurrentThread.ManagedThreadId;

//            var value = $"这是来自线程{threadId}的数据";
//            _threadStatic ??= value;
//            _threadLocal.Value ??= value;
//            Console.WriteLine($"Use ThreadStaticAttribute; Thread：{threadId}; Value：{_threadStatic}");
//            Console.WriteLine($"Use ThreadLocal;           Thread：{threadId}; Value：{_threadLocal.Value}");
//        });

//        Console.Read();
//    }
//}