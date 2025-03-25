namespace CustomMutlpPipeline.Basic.Interfaces
{
	public interface IPipelineProvider
	{
		public Pipeline GetPipeline(string name);
	}

	public interface IPipelineContainer
	{
		public void Add(string name, Action<IPipelineBuilder> builderAction);

		public void Add<TPipelineBuilder>()
			where TPipelineBuilder : SpecifyPipelineBuilder, new();
	}
}
