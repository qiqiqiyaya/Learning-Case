using Autofac;

namespace SimplePipeline.Core
{
    public class PipelineBuilder : IPipelineBuilder
    {
        private readonly ILifetimeScope _serviceProvider;

        public PipelineBuilder(ILifetimeScope serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IPipeline Build(Action<PipelineConfiguration> config)
        {
            var configuration = new PipelineConfiguration();
            config(configuration);

            var pipeline = _serviceProvider.Resolve<IPipeline>();
            pipeline.Init(configuration);
            return pipeline;
        }
    }

}
