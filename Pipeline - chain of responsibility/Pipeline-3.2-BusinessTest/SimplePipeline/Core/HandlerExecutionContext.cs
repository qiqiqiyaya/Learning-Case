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
            // Need improvement
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

        public async Task<bool> RuleValidateAsync<T>()
            where T : Rule.Rule
        {
            var rule = GetRequiredService<T>();
            return await rule.ValidateAsync(this);
        }

        public async Task<bool> And(params Type[] rules)
        {
            foreach (var rule in rules)
            {
                if (rule == typeof(Rule.Rule))
                {
                    var rl = ServiceProvider.Resolve(rule) as Rule.Rule;
                    if (!await rl.ValidateAsync(this))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public async Task<bool> Or(params Type[] rules)
        {
            foreach (var rule in rules)
            {
                if (rule == typeof(Rule.Rule))
                {
                    var rl = ServiceProvider.Resolve(rule) as Rule.Rule;
                    if (await rl.ValidateAsync(this))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}