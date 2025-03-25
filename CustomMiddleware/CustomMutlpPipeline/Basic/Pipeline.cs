using CustomMutlpPipeline.Basic.Interfaces;

namespace CustomMutlpPipeline.Basic
{
	public class Pipeline : IPipeline
	{
		public string Name { get; }

		private readonly Func<PipelineContext, ValueTask> _pipeline;

		public Pipeline(Func<PipelineContext, ValueTask> pipeline, string name)
		{
			_pipeline = pipeline;
			Name = name;
		}

		public ValueTask ExecuteAsync(PipelineContext context)
		{
			return _pipeline(context);
		}
	}
}
