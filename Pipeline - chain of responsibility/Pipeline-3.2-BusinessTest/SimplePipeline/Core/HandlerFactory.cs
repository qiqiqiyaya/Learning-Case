using Autofac;

namespace SimplePipeline.Core
{
    public class HandlerFactory : IHandlerFactory
    {
        private readonly ILifetimeScope _serviceProvider;

        public HandlerFactory(ILifetimeScope serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IHandler Create(Subject subject)
        {
            // resolve IHandler by subject 
            // logical judgment here
            return _serviceProvider.Resolve<IHandler>();
        }
    }
}
