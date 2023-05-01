namespace SimplePipeline.Core
{
    public interface ISubjectFactory
    {
        IEnumerable<StepMap> Create(List<SubjectData> data);
    }
}
