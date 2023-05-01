namespace ChainOfResponsibility.Core
{
    public interface IHandler
    {
        void SetNext(IHandler? next);

        Task ExecuteAsync(PipelineContext context);
    }
}
