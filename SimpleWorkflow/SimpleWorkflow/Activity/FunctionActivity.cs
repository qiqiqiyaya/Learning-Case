namespace SimpleWorkflow.Activity
{
    public class FunctionActivity : Activity
    {
        public FunctionActivity(Func<Task> action)
        {
            Action = action;
        }

        public Func<Task> Action { get; set; }

        public override async Task ExecuteAsync()
        {
            await Action();
        }
    }

    public static class FunctionActivityExtensions
    {
        public static IWorkflow Function(this IWorkflow workflow, Func<Task> action)
        {
            workflow.Then(new FunctionActivity(action));
            return workflow;
        }
    }
}
