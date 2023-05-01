using ChainOfResponsibility.Core;

namespace ChainOfResponsibility
{
    public class SubjectHandlerFactory : IHandlerMapFactory
    {
        public IEnumerable<IHandler> Create(List<Subject> data)
        {
            return data.Select(s => new SubjectHandler(s));
        }
    }
}
