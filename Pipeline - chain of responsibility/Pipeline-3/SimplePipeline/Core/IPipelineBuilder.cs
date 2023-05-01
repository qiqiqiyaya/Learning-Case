namespace SimplePipeline.Core
{
    public interface IPipelineBuilder
    {
        IPipeline Build(List<Subject> data);
    }
}
