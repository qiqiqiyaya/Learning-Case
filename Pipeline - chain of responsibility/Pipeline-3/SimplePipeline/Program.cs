// See https://aka.ms/new-console-template for more information

using Autofac;
using SimplePipeline.BusinessTest;
using SimplePipeline.Core;

ContainerBuilder autoBuilder = new ContainerBuilder();
autoBuilder.RegisterType<SubjectHandlerMapFactory>().As<ISubjectHandlerMapFactory>().SingleInstance();
autoBuilder.RegisterType<Pipeline>().As<IPipeline>().InstancePerDependency();
autoBuilder.RegisterType<SubjectHandler>().As<IHandler>().InstancePerDependency();
autoBuilder.RegisterType(typeof(PipelineBuilder)).As<IPipelineBuilder>().InstancePerDependency();
var container = autoBuilder.Build();

await using var scope = container.BeginLifetimeScope();
var pipelineBuilder= scope.Resolve<IPipelineBuilder>();
var subjectData = Subject.TestData();
var pipeline = pipelineBuilder.Build(subjectData);
await pipeline.RunAsync();
Console.Read();