using SimplePipeline.Core;

namespace SimplePipeline.Rule
{
    public class FunctionActivity : Activity
    {
        public FunctionActivity(Func<HandlerExecutionContext, Task> action)
        {
            Action = action;
        }

        public Func<HandlerExecutionContext, Task> Action { get; set; }

        public override async Task ExecuteAsync(HandlerExecutionContext context)
        {
            await Action(context);
        }
    }

    public static class FunctionActivityExtensions
    {
        public static IWorkflow Function(this IWorkflow workflow, Func<HandlerExecutionContext, Task> action)
        {
            workflow.Then(new FunctionActivity(action));
            return workflow;
        }
    }
}
