using SimplePipeline.Core;

namespace SimplePipeline.Rule
{
    public class If : Activity
    {
        public If(Func<HandlerExecutionContext, Task<bool>> condition, IActivity then, IActivity @else)
        {
            Condition = condition;
            Then = then;
            Else = @else;
        }

        public Func<HandlerExecutionContext, Task<bool>> Condition { get; set; }

        public IActivity Then { get; set; }

        public IActivity Else { get; set; }

        public override async Task ExecuteAsync(HandlerExecutionContext context)
        {
            Console.Write("执行");
            var next = await Condition(context) ? Then : Else;

            await next.ExecuteAsync(context);
        }
    }

    public static class IfExtensions
    {
        public static IWorkflow If(this IWorkflow workflow, Func<HandlerExecutionContext, Task<bool>> condition, IActivity then, IActivity @else)
        {
            workflow.Then(new If(condition, then, @else));
            return workflow;
        }
    }
}
