namespace SimplePipeline.Rule.Workflow
{
    public interface IWorkflow : IActivity
    {
        public List<IActivity> ActivityContainer { get; }

        public Task Run(RuleCalculationContext context);

        public IWorkflow Then(IActivity activity);
    }
}
