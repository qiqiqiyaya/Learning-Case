using Microsoft.Extensions.Logging;

namespace ConsoleApp1
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
            ILogger logger = factory.CreateLogger("Program");
            LogStartupMessage(logger, new Test() { aaa = "1312", bbb = 11 });
            Console.WriteLine("Hello, World!");

            Console.ReadLine();
        }

        [LoggerMessage(0, Level = LogLevel.Information, Message = "Hello World! Logging is {@Description}.")]
        static partial void LogStartupMessage(ILogger logger, Test description);


        public class Test
        {
            public string aaa { get; set; }

            public int bbb { get; set; }
        }
    }
}
