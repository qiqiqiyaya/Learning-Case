using System.ComponentModel.Design;
using CustomMutlpPipeline.PipelineBuilders;
using CustomMutlpPipeline.Pipes;
using Microsoft.Extensions.DependencyInjection;

namespace CustomMutlpPipeline
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            ServiceCollection container = new ServiceCollection();
            container.AddTransient<IPipelineBuilder>(PipelineBuilder.Create);
            container.AddTransient<IPipelineProvider, PipelineProvider>();
            var provider = container.BuildServiceProvider();


            var builder = PipelineBuilder.Create(provider);
            builder.Use<TestMiddleware>();
            builder.Use(context =>
            {
                Console.WriteLine("test2");
                return ValueTask.CompletedTask;
            });
            builder.Use(new SubjectPipe());

            var pipeline = builder.Build("testPipeline");
            await pipeline.ExecuteAsync(new PipelineContext());

            TaBuilder tb = new TaBuilder();
            Console.Read();
        }
    }
}
