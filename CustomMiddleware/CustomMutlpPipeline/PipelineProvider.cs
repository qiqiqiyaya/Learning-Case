using CustomMutlpPipeline.Basic;
using CustomMutlpPipeline.Basic.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CustomMutlpPipeline
{
	public class PipelineProvider : IPipelineProvider, IPipelineContainer
	{
		private Dictionary<string, Pipeline> Pipelines { get; } = new Dictionary<string, Pipeline>();
		private readonly IServiceProvider _serviceProvider;

		public PipelineProvider(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public void Add(string name, Action<IPipelineBuilder> builderAction)
		{
			var builder = _serviceProvider.GetRequiredService<IPipelineBuilder>();
			builderAction(builder);

			var pipeline = builder.Build(name);
			AddPipeline(pipeline);
		}

		public void Add<TPipelineBuilder>()
			 where TPipelineBuilder : SpecifyPipelineBuilder, new()
		{
			var builder = new TPipelineBuilder();
			builder.Build(this);
		}

		private void AddPipeline(Pipeline pipeline)
		{
			Pipelines.Add(pipeline.Name, pipeline);
		}

		public Pipeline GetPipeline(string name)
		{
			return Pipelines[name];
		}
	}
}
