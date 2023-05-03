using Autofac;
using SimplePipeline.Scope;

namespace SimplePipeline.Core
{
    /// <summary>
    /// subject executing context
    /// </summary>
    public class HandlerExecutionContext
    {
        public HandlerExecutionContext(PipelineContext pipelineContext, Subject subject)
        {
            PipelineContext = pipelineContext;
            Subject = subject;
        }

        public PipelineContext PipelineContext { get; }

        public ILifetimeScope ServiceProvider => PipelineContext.ServiceProvider;

        public Subject Subject { get; }

        public Employee Employee => PipelineContext.Employee;

        public List<Scope.Scope> Scopes => Subject.Scopes;

        public T GetRequiredService<T>()
            where T : notnull
        {
            return ServiceProvider.Resolve<T>();
        }

        public Task<ScopeCheckResult> ScopeCheck()
        {
            var checker = GetRequiredService<IScopeCheckerService>();
            return checker.CheckAsync(Subject, Employee);
        }

        public void AddLog<THandler>(string log)
            where THandler : IHandler
        {
            PipelineContext.AddExecutingLog($"{DateTime.Now.ToString("s")} - {nameof(THandler)}: {log}");
        }
    }
}