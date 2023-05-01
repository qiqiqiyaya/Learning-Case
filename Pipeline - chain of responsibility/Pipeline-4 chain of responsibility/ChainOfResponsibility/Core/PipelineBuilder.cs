using Autofac;

namespace ChainOfResponsibility.Core
{
    public class PipelineBuilder : IPipelineBuilder
    {
        private readonly IHandlerMapFactory _handlerMapFactory;
        private readonly ILifetimeScope _serviceProvider;

        public PipelineBuilder(IHandlerMapFactory handlerMapFactory, ILifetimeScope serviceProvider)
        {
            _handlerMapFactory = handlerMapFactory;
            _serviceProvider = serviceProvider;
        }

        public IPipeline Build(List<Subject> data)
        {
            var subs = _handlerMapFactory.Create(data);

            var pipeline = _serviceProvider.Resolve<IPipeline>();
            foreach (var handler in subs)
            {
                pipeline.AddHandler(handler);
            }

            return pipeline;
        }
    }

}
