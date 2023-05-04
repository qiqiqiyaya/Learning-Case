namespace SimplePipeline.Core
{
    public interface IPipelineBuilder
    {
        IPipeline Build(Action<PipelineConfiguration> config);
    }
}
