using SimplePipeline.Core;

namespace SimplePipeline.Rule.Workflow
{
    public interface IActivity
    {
        public Task ExecuteAsync(RuleCalculationContext context);
    }
}
