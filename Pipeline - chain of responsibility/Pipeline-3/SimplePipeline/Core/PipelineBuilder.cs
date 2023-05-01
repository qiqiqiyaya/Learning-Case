using Autofac;
using System;

namespace SimplePipeline.Core
{
    public class PipelineBuilder : IPipelineBuilder
    {
        private readonly ISubjectHandlerMapFactory _subjectHandlerMapFactory;
        private readonly ILifetimeScope _serviceProvider;

        public PipelineBuilder(ISubjectHandlerMapFactory subjectHandlerMapFactory, ILifetimeScope serviceProvider)
        {
            _subjectHandlerMapFactory = subjectHandlerMapFactory;
            _serviceProvider = serviceProvider;
        }

        public IPipeline Build(List<Subject> data)
        {
            var handlerMaps = _subjectHandlerMapFactory.Create(data);

            var pipeline = _serviceProvider.Resolve<IPipeline>();
            pipeline.AddHandlers(handlerMaps);
            return pipeline;
        }
    }

}
