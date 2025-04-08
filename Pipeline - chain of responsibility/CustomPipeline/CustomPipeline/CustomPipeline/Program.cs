using CustomPipeline.Customs;
using CustomPipeline.PipeLine.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace CustomPipeline
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            ServiceCollection service = new ServiceCollection();

            var builder = PipeRegisterBuilder.Create();
            builder.AddPipeLine<TestPipelineStartup, TestPipeline, CustomPipelineContext>();
            builder.Build(service);

            var serviceProvider = service.BuildServiceProvider();

            using var scope = serviceProvider.CreateScope();
            var startup = (IPipelineStartup)scope.ServiceProvider.GetRequiredService(typeof(TestPipelineStartup));

            await startup.StartAsync();


            Console.ReadKey();
        }
    }
}
