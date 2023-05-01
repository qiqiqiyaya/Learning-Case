using Autofac;
using SimplePipeline.Core;

namespace SimplePipeline.BusinessTest
{
    /// <summary>
    /// SubjectExecutionContext
    /// </summary>
    public class SubjectExecutionContext : IHandlerExecutionContext
    {
        public SubjectExecutionContext(SalaryPipelineContext pipelineContext, Subject subject)
        {
            PipelineContext = pipelineContext;
            Subject = subject;
        }

        /// <summary>
        /// SalaryPipelineContext
        /// </summary>
        public SalaryPipelineContext PipelineContext { get; }

        public ILifetimeScope ServiceProvider => PipelineContext.ServiceProvider;

        public Subject Subject { get; }

        public T GetRequiredService<T>()
            where T : notnull
        {
            return ServiceProvider.Resolve<T>();
        }

        public void AddExecutingLog(string log)
        {
            PipelineContext.ExecutingLog.Add(log);
        }
    }
}