// See https://aka.ms/new-console-template for more information

using System.Reflection.Emit;
using Autofac;
using SimplePipeline;

Console.WriteLine("Hello, World!");

ContainerBuilder autoBuilder = new ContainerBuilder();

autoBuilder.RegisterType(typeof(AttendanceSubjectStep)).As<IStep<DataContext>>().AsSelf().SingleInstance();
autoBuilder.RegisterType(typeof(SubsidySubjectStep)).As<IStep<DataContext>>().AsSelf().SingleInstance();
var container = autoBuilder.Build();
var scope = container.BeginLifetimeScope();



var builder = new PipelineBuilder(scope,
    new Type[] { typeof(AttendanceSubjectStep), typeof(SubsidySubjectStep) });

var pipe = builder.CreatePipeline(new DataContext(scope));
await pipe.ExecuteAsync();

Console.Read();
//var aa = Delegate.CreateDelegate(typeof(AttendanceSubjectStep), new DynamicMethod(""));
//aa.DynamicInvoke();


public class DataContext : IDataContext
{
    public DataContext(ILifetimeScope serviceProvider)
    {
        DynamicProperties = new Dictionary<string, object>();
        ServiceProvider = serviceProvider;
    }

    public ILifetimeScope ServiceProvider { get; set; }
    public Dictionary<string, object> DynamicProperties { get; set; }
}

public class AttendanceSubjectStep : IStep<DataContext>
{
    public Task ExecuteAsync(DataContext context)
    {
        Console.WriteLine("AttendanceSubjectStep");
        context.DynamicProperties.Add(nameof(AttendanceSubjectStep), "AttendanceSubjectStep");
        return Task.CompletedTask;
    }
}

public class SubsidySubjectStep : IStep<DataContext>
{
    public Task ExecuteAsync(DataContext context)
    {
        Console.WriteLine("SubsidySubjectStep");
        context.DynamicProperties.Add(nameof(SubsidySubjectStep), "SubsidySubjectStep");
        return Task.CompletedTask;
    }
}