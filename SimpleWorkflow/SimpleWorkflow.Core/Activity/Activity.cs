namespace SimpleWorkflow.Core.Activity
{
    public abstract class Activity : IActivity
    {
        protected Activity()
        {
            Type = GetType().Name;
        }

        public string Id { get; set; } = default!;

        public string Type { get; set; }

        public virtual Task ExecuteAsync()
        {
            return Task.CompletedTask;
        }
    }
}
