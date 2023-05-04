using SimplePipeline.Core;

namespace SimplePipeline.Rule.Workflow
{
    public class Workflow : Activity, IWorkflow
    {
        public Workflow()
            : this(new List<IActivity>())
        {

        }

        public Workflow(List<IActivity> activities)
        {
            ActivityContainer = activities;
        }

        public List<IActivity> ActivityContainer { get; }

        public async Task Run(RuleCalculationContext context)
        {
            var isComplete = false;
            var index = 0;

            while (!isComplete)
            {
                var activity = ActivityContainer[index];
                await activity.ExecuteAsync(context);

                index++;
                if (index == ActivityContainer.Count)
                {
                    isComplete = true;
                }
            }
        }

        public IWorkflow Then(IActivity activity)
        {
            ActivityContainer.Add(activity);
            return this;
        }

        public override async Task ExecuteAsync(RuleCalculationContext context)
        {
            await Run(context);
        }
    }
}
