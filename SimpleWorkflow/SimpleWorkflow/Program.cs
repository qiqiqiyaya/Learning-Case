// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using SimpleWorkflow;
using SimpleWorkflow.Activity;
using SimpleWorkflow.Core;


var collection = new ServiceCollection();


Console.WriteLine("Hello, World!");


IWorkflow workflow = new Workflow();

workflow.If(() => 18 > 0,
    new Workflow(new List<IActivity>()
    {
        new If(()=> 22 > 0,
            new FunctionActivity(() =>
            {
                Console.WriteLine("111");
                return Task.CompletedTask;
            }),
            new FunctionActivity(() =>
            {
                Console.WriteLine("222");
                return Task.CompletedTask;
            }))
    }),
    new FunctionActivity(() =>
    {
        Console.WriteLine("3333");
        return Task.CompletedTask;
    }));

await workflow.Run();

Console.Read();