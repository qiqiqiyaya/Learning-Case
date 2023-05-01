using Autofac;

namespace SimplePipeline.Core
{
    /// <summary>
    /// subject executing context
    /// </summary>
    public class SubjectExecutionContext
    {
        public SubjectExecutionContext(SalaryContext salaryContext, SubjectData subjectData)
        {
            SalaryContext = salaryContext;
            SubjectData = subjectData;
        }

        public SalaryContext SalaryContext { get; }

        public ILifetimeScope ServiceProvider => SalaryContext.ServiceProvider;

        public SubjectData SubjectData { get; }

        public T GetRequiredService<T>()
            where T : notnull
        {
            return ServiceProvider.Resolve<T>();
        }
    }
}