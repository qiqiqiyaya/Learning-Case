// See https://aka.ms/new-console-template for more information

using Autofac;
using SimplePipeline.Core;

ContainerBuilder autoBuilder = new ContainerBuilder();
autoBuilder.RegisterType<HandlerMapFactory>().As<IHandlerMapFactory>().InstancePerDependency();
autoBuilder.RegisterType<Pipeline>().As<IPipeline>().InstancePerDependency();
autoBuilder.RegisterType<SimplePipeline.SubjectHandler>().As<IHandler>().InstancePerDependency();
autoBuilder.RegisterType<PipelineBuilder>().As<IPipelineBuilder>().InstancePerDependency();
var container = autoBuilder.Build();

await using var scope = container.BeginLifetimeScope();

var pipelineBuilder= scope.Resolve<IPipelineBuilder>();
var subjectData = Subject.TestData();
var pipeline = pipelineBuilder.Build(subjectData);
await pipeline.RunAsync();
Console.Read();
