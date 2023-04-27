// See https://aka.ms/new-console-template for more information

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

var pipe = builder.CreatePipeline(new DataContext());
await pipe.ExecuteAsync();

Console.Read();




public class DataContext : IDataContext
{
    public DataContext()
    {
        DynamicProperties = new Dictionary<string, object>();
    }

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