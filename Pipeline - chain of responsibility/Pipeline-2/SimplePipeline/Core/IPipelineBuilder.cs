namespace SimplePipeline.Core
{
    public interface IPipelineBuilder
    {
        IPipeline Create(List<SubjectData> data);
    }
}
