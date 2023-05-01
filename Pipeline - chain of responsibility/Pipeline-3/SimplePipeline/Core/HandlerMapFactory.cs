using Autofac;

namespace SimplePipeline.Core
{
    public class HandlerMapFactory : IHandlerMapFactory
    {
        private readonly ILifetimeScope _serviceProvider;

        public HandlerMapFactory(ILifetimeScope serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IEnumerable<HandlerMap> Create(List<Subject> data)
        {
            return data.Select(s => new HandlerMap(_serviceProvider.Resolve<IHandler>(), s));
        }
    }
}
