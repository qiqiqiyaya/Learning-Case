using log4net;
using Quartz;

namespace OutputLog
{
    public class HelloJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Delay(1000);
            var index = context.MergedJobDataMap["index"].ToString();
            Container.List.Add(Convert.ToInt32(index));

            Logger.Log.Info("count:" + Container.List.Count + "  index：" + index);
            await Console.Out.WriteLineAsync("Greetings from HelloJob!");
        }
    }

    public static class Logger
    {
        private static ILog _log = log4net.LogManager.GetLogger("test");

        public static ILog Log => _log;
    }


    public static class Container
    {
        public static List<int> List = new List<int>();
    }
}
