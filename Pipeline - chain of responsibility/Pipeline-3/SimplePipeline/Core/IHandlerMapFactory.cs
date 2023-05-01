namespace SimplePipeline.Core
{
    public interface IHandlerMapFactory
    {
        IEnumerable<HandlerMap> Create(List<Subject> data);
    }
}
