namespace SimplePipeline.Core
{
    public interface IHandlerFactory
    {
        IHandler Create(Subject subject);
    }
}
