using CustomMiddleware.Pipes;
using System.ComponentModel.Design;

namespace CustomMiddleware
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            ServiceContainer container = new ServiceContainer();

            var builder = PipelineBuilder.Create(container);

            builder.Use<TestMiddleware>();
            builder.Use(context =>
            {
                Console.WriteLine("test2");
                return ValueTask.CompletedTask;
            });
            builder.Use(new SubjectPipe());

            var pipeline = builder.Build("testPipeline");
            await pipeline.ExecuteAsync(new PipelineContext());


            Console.Read();
        }
    }
}
