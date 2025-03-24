namespace CustomMutlpPipeline
{
    public class PipelineProvider : IPipelineProvider
    {
        private Dictionary<string, Pipeline> Pipelines { get; } = new Dictionary<string, Pipeline>();
        private readonly IServiceProvider _serviceProvider;

        public PipelineProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void AddPipeline(string name, Action<PipelineBuilder> builderAction)
        {
            var builder = PipelineBuilder.Create(_serviceProvider);
            builderAction(builder);

            var pipeline = builder.Build(name);
            AddPipeline(pipeline);
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
