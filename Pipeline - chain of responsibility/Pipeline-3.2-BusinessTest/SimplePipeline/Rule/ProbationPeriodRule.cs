using SimplePipeline.Core;

namespace SimplePipeline.Rule
{
    public class ProbationPeriodRule : Rule
    {
        public override Task<bool> ValidateAsync(HandlerExecutionContext context)
        {
            return Task.FromResult(true);
        }
    }
}
