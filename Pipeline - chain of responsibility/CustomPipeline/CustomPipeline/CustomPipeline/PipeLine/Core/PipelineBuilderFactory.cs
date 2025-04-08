namespace CustomPipeline.PipeLine.Core
{
    public class PipelineBuilderFactory : IPipelineBuilderFactory
    {
        public IPipelineBuilder<TContext> Create<TContext>(string pipelineName)
        {
            return new PipelineBuilder<TContext>(pipelineName);
        }
    }
}
