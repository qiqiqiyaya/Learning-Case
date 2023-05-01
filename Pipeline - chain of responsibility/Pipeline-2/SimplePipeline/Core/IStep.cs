namespace SimplePipeline.Core
{
    public interface IStep
    {
        Task ExecuteAsync(SubjectExecutionContext context);
    }
}
