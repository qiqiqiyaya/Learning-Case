using SimplePipeline.Core;

namespace SimplePipeline.Rule.Rules
{
    public class ProbationPeriodRule : Rule
    {
        public override Task<bool> CalculateAsync(HandlerExecutionContext context)
        {
            return Task.FromResult(true);
        }
    }
}
