using Autofac;
using SimplePipeline.Rule;
using SimplePipeline.Scope;
using SimplePipeline.Scope.Services;

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
            CheckLogs = new List<ScopeCheckLog>();
        }

        protected PipelineContext PipelineContext { get; }

        public ILifetimeScope ServiceProvider => PipelineContext.ServiceProvider;

        public Subject Subject { get; }

        public Employee Employee => PipelineContext.Employee;


        public T GetRequiredService<T>()
            where T : notnull
        {
            // TODO Need improvement
            return ServiceProvider.Resolve<T>();
        }

        public List<ScopeCheckLog> CheckLogs { get; }

        public SubjectExecutedResult Result { get; } = new SubjectExecutedResult();

        public Task<ScopeCheckResult> ScopeCheck()
        {
            var checker = GetRequiredService<IScopeCheckerService>();
            return checker.CheckAsync(new ScopeCheckContext(this));
        }

        public void AddLog<THandler>(string log)
            where THandler : IHandler
        {
            PipelineContext.AddExecutingLog($"{DateTime.Now.ToString("s")} - {nameof(THandler)}: {log}");
        }

        public async Task RuleExecuteAsync()
        {
            var calculateService = GetRequiredService<IRuleScheduleService>();
            await calculateService.CalculateAsync(new RuleCalculationContext(this));
        }
    }
}