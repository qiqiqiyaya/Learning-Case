using Autofac;

namespace SimplePipeline.Core
{
    public class PipelineBuilder : IPipelineBuilder
    {
        private readonly ISubjectFactory _subjectFactory;
        private readonly ILifetimeScope _serviceProvider;

        public PipelineBuilder(ISubjectFactory subjectFactory, ILifetimeScope serviceProvider)
        {
            _subjectFactory = subjectFactory;
            _serviceProvider = serviceProvider;
        }

        public IPipeline Create(List<SubjectData> data)
        {
            var subs = _subjectFactory.Create(data);

            var pipeline = _serviceProvider.Resolve<IPipeline>();
            pipeline.AddSteps(subs);
            return pipeline;
        }
    }

}
