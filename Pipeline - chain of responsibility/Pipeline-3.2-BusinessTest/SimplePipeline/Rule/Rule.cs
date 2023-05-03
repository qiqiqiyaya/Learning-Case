using SimplePipeline.Core;

namespace SimplePipeline.Rule
{
    public abstract class Rule
    {
        public abstract Task<bool> ValidateAsync(HandlerExecutionContext context);
    }
}
