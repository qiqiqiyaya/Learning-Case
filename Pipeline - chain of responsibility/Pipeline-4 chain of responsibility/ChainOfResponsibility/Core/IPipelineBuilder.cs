namespace ChainOfResponsibility.Core
{
    public interface IPipelineBuilder
    {
        IPipeline Build(List<Subject> data);
    }
}
