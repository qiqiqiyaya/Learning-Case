using SimplePipeline.Core;

namespace SimplePipeline.Rule
{
    public class EmployeeGroupRule : Rule
    {
        public int RuleKey { get; set; }

        public List<string> EmployeeGroup { get; set; }

        public override Task<bool> ValidateAsync(HandlerExecutionContext context)
        {
            return Task.FromResult(true);
        }
    }
}
