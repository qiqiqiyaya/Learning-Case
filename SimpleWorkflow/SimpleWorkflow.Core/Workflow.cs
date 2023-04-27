namespace SimpleWorkflow.Core
{
    public class Workflow : Core.Activity.Activity, IWorkflow
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

        public async Task Run()
        {
            var isComplete = false;
            var index = 0;

            while (!isComplete)
            {
                var activity = ActivityContainer[index];
                await activity.ExecuteAsync();

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

        public override async Task ExecuteAsync()
        {
            await Run();
        }
    }
}
