namespace SimplePipeline.Core
{
    public interface IHandler
    {
        Task ExecuteAsync(HandlerExecutionContext context);
    }
}
