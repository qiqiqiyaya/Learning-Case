using Autofac;

namespace SimplePipeline
{
    public abstract class PipelineBuilderBase<T> : IPipelineBuilder<T>
        where T : IDataContext
    {
        private readonly ILifetimeScope _serviceProvider;

        protected PipelineBuilderBase(ILifetimeScope serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IPipeline CreatePipeline(T dataContext)
        {
            var steps = StepsTypes.Select(s =>
            {
                return (IStep<T>)_serviceProvider.Resolve(s);
            });

            var pipeline = new Pipeline<T>(dataContext);
            pipeline.AddSteps(steps);
            return pipeline;
        }

        protected abstract Type[] StepsTypes { get; }
    }

    public class PipelineBuilder : PipelineBuilderBase<DataContext>
    {
        public PipelineBuilder(ILifetimeScope serviceProvider, Type[] stepsTypes)
            : base(serviceProvider)
        {
            StepsTypes = stepsTypes;
        }

        protected override Type[] StepsTypes { get; }
    }
}
