namespace SimplePipeline.Rule.Workflow
{
    public abstract class Activity : IActivity
    {
        public virtual Task ExecuteAsync(RuleCalculationContext context)
        {
            return Task.CompletedTask;
        }
    }
}