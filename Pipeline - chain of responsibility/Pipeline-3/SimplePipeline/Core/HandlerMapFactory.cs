using Autofac;

namespace SimplePipeline.Core
{
    public class SubjectHandlerMapFactory : ISubjectHandlerMapFactory
    {
        private readonly ILifetimeScope _serviceProvider;

        public SubjectHandlerMapFactory(ILifetimeScope serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IEnumerable<HandlerMap> Create(List<Subject> data)
        {
            return data.Select(s => new HandlerMap(_serviceProvider.Resolve<IHandler>(), s));
        }
    }
}
