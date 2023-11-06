using System.Collections.Specialized;
using log4net;
using log4net.Core;
using log4net.Repository.Hierarchy;
using OutputLog;
using Quartz;
using Quartz.Impl;


FileInfo info = new FileInfo(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + "Log4Net.config");
if (!info.Exists)
{
    throw new Exception("No Log4Net.config file.");
}

log4net.Config.XmlConfigurator.Configure(info);

NameValueCollection props = new NameValueCollection
{
    { "quartz.serializer.type", "binary" }
};
StdSchedulerFactory factory = new StdSchedulerFactory(props);

// get a scheduler
IScheduler sched = await factory.GetScheduler();
await sched.Start();

// define the job and tie it to our HelloJob class
IJobDetail job = JobBuilder.Create<HelloJob>()
    .WithIdentity("myJob", "group1")
    .StoreDurably()
    .Build();

await sched.AddJob(job, true);

for (int i = 0; i < 150; i++)
{
    await Task.Delay(500);
    // Trigger the job to run now, and then every 40 seconds
    ITrigger trigger = TriggerBuilder.Create()
        .WithIdentity(Guid.NewGuid().ToString("N"), "group1")
        .UsingJobData("index", i)
        .StartNow()
        .ForJob("myJob", "group1")
        .Build();

    await sched.ScheduleJob(trigger);
}

Console.WriteLine("结束");
Console.Read();