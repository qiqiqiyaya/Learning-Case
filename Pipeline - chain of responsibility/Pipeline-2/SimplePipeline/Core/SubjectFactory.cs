using Autofac;

namespace SimplePipeline.Core
{
    public class SubjectFactory : ISubjectFactory
    {
        private readonly ILifetimeScope _serviceProvider;

        public SubjectFactory(ILifetimeScope serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IEnumerable<StepMap> Create(List<SubjectData> data)
        {
            return data.Select(s => new StepMap(_serviceProvider.Resolve<IStep>(), s));
        }
    }
}
