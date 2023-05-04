namespace SimplePipeline.Rule.Workflow
{
    public class ActionActivity : Activity
    {
        public ActionActivity(Func<RuleCalculationContext, Task> action)
        {
            Action = action;
        }

        public Func<RuleCalculationContext, Task> Action { get; set; }

        public override async Task ExecuteAsync(RuleCalculationContext context)
        {
            await Action(context);
        }
    }

    public static class FunctionActivityExtensions
    {
        public static IWorkflow Function(this IWorkflow workflow, Func<RuleCalculationContext, Task> action)
        {
            workflow.Then(new ActionActivity(action));
            return workflow;
        }
    }
}
