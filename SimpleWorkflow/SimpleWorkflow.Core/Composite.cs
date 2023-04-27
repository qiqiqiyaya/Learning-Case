namespace SimpleWorkflow.Core
{
    public abstract class Composite : Core.Activity.Activity
    {
        public ICollection<IActivity> Activities { get; set; } = new HashSet<IActivity>();
    }
}
