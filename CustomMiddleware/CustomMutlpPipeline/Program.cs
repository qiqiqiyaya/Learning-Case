using CustomMutlpPipeline.Basic;
using CustomMutlpPipeline.Basic.Interfaces;
using CustomMutlpPipeline.Extensions;
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
			container.AddPipeline(pipelineProvider =>
			{
				pipelineProvider.Add<TaBuilder>();
				pipelineProvider.Add("test2", builder =>
				{
					builder.Use<TestMiddleware>();
					builder.Use(context =>
					{
						Console.WriteLine("test2");
						return ValueTask.CompletedTask;
					});
					builder.Use(new SubjectPipe());
				});

			});
			var provider = container.BuildServiceProvider();

			var pipelineProvider = provider.GetRequiredService<IPipelineProvider>();
			var pipeline = pipelineProvider.GetPipeline("test2");

			await pipeline.ExecuteAsync(new PipelineContext());
			Console.Read();
		}
	}
}
