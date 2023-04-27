namespace SimpleWorkflow.Core
{
    public interface IWorkflow : IActivity
    {
        public List<IActivity> ActivityContainer { get; }

        public Task Run();

        public IWorkflow Then(IActivity activity);
    }
}
