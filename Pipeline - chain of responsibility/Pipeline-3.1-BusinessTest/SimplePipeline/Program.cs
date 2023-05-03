// See https://aka.ms/new-console-template for more information

using Autofac;
using SimplePipeline;
using SimplePipeline.Core;
using SimplePipeline.Scope;

ContainerBuilder autoBuilder = new ContainerBuilder();
autoBuilder.RegisterType<HandlerFactory>().As<IHandlerFactory>().InstancePerDependency();
autoBuilder.RegisterType<Pipeline>().As<IPipeline>().InstancePerDependency();
autoBuilder.RegisterType<SubjectHandler>().As<IHandler>().InstancePerDependency();
autoBuilder.RegisterType<PipelineBuilder>().As<IPipelineBuilder>().InstancePerDependency();
autoBuilder.RegisterType<ScopeCheckerService>().As<IScopeCheckerService>().InstancePerLifetimeScope();
var container = autoBuilder.Build();

await using var scope = container.BeginLifetimeScope();

var pipelineBuilder = scope.Resolve<IPipelineBuilder>();

var pipeline = pipelineBuilder.Build(config =>
{
    config.Subjects = Subject.TestData();
    config.Employee = Employee.TestData();
});
await pipeline.RunAsync();

pipeline.Context.ExecutingLog.ForEach(Console.WriteLine);

Console.Read();
