using System.Collections.Specialized;
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
    .Build();

// Trigger the job to run now, and then every 40 seconds
ITrigger trigger = TriggerBuilder.Create()
    .WithIdentity("myTrigger", "group1")
    .StartNow()
    //.WithSimpleSchedule(x => x
    //    .WithIntervalInSeconds(40)
    //    .RepeatForever())
    .Build();

await sched.ScheduleJob(job, trigger);
Console.Read();