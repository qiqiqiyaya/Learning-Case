namespace CustomPipeline.PipeLine.Core
{
    public interface IPipelineBuilderFactory
    {
        IPipelineBuilder<TContext> Create<TContext>(string pipelineName);
    }
}
