namespace ChainOfResponsibility.Core
{
    public interface IHandlerMapFactory
    {
        IEnumerable<IHandler> Create(List<Subject> data);
    }
}
