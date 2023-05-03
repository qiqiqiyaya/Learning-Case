using SimplePipeline.Core;

namespace SimplePipeline.Rule
{
    public interface IWorkflow : IActivity
    {
        public List<IActivity> ActivityContainer { get; }

        public Task Run(HandlerExecutionContext context);

        public IWorkflow Then(IActivity activity);
    }
}
