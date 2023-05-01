using Autofac;
using ChainOfResponsibility;
using ChainOfResponsibility.Core;

ContainerBuilder autoBuilder = new ContainerBuilder();
autoBuilder.RegisterType<SubjectHandlerFactory>().As<IHandlerMapFactory>().InstancePerDependency();
autoBuilder.RegisterType<Pipeline>().As<IPipeline>().InstancePerDependency();
autoBuilder.RegisterType<PipelineBuilder>().As<IPipelineBuilder>().InstancePerDependency();
var container = autoBuilder.Build();

await using var scope = container.BeginLifetimeScope();

var pipelineBuilder = scope.Resolve<IPipelineBuilder>();
var subjectData = Subject.TestData();
var pipeline = pipelineBuilder.Build(subjectData);
await pipeline.RunAsync();
Console.Read();