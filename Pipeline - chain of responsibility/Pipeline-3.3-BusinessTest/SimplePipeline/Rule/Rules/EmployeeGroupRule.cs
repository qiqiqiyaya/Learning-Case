using SimplePipeline.Core;

namespace SimplePipeline.Rule.Rules
{
    public class EmployeeGroupRule : Rule
    {
        public int RuleKey { get; set; }

        public List<string> EmployeeGroup { get; set; }

        public override Task<bool> CalculateAsync(HandlerExecutionContext context)
        {
            return Task.FromResult(true);
        }
    }
}
