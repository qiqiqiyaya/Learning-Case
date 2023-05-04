using SimplePipeline.Core;

namespace SimplePipeline.Rule
{
    public class RuleCalculationContext
    {
        public RuleCalculationContext(HandlerExecutionContext context)
        {
            Context = context;
        }

        public List<OriginalRule> Rules => Context.Subject.Rules;

        public Employee Employee => Context.Employee;

        protected HandlerExecutionContext Context { get; }

        public T GetRequiredService<T>()
            where T : notnull
        {
            return Context.GetRequiredService<T>();
        }

        public List<Rule> GetRuleTrees()
        {
            var ruleResolveService = GetRequiredService<IRuleResolveService>();
            return ruleResolveService.ConvertTree(Rules);
        }


    }
}
