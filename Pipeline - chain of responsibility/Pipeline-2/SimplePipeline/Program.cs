// See https://aka.ms/new-console-template for more information

using Autofac;
using SimplePipeline;
using SimplePipeline.Core;

Console.WriteLine("Hello, World!");

ContainerBuilder autoBuilder = new ContainerBuilder();
autoBuilder.RegisterType<SubjectFactory>().As<ISubjectFactory>().SingleInstance();
autoBuilder.RegisterType<Pipeline>().As<IPipeline>().InstancePerDependency();
autoBuilder.RegisterType<Subject>().As<IStep>().InstancePerDependency();
autoBuilder.RegisterType<PipelineBuilder>().As<IPipelineBuilder>().InstancePerDependency();
var container = autoBuilder.Build();

var pipelineBuilder= container.Resolve<IPipelineBuilder>();
var subjectData = SubjectData.TestData();
var pipeline = pipelineBuilder.Create(subjectData);
await pipeline.ExecuteAsync();
Console.Read();
