using SimpleWorkflow.Core;

namespace SimpleWorkflow.Activity
{
    public class If : Core.Activity.Activity
    {
        public If(Func<bool> condition, IActivity then, IActivity @else)
        {
            Condition = condition;
            Then = then;
            Else = @else;
        }

        public Func<bool> Condition { get; set; }

        public IActivity Then { get; set; }

        public IActivity Else { get; set; }

        public override async Task ExecuteAsync()
        {
            Console.Write("执行");
            var next = Condition() ? Then : Else;

            await next.ExecuteAsync();
        }
    }

    public static class IfExtensions
    {
        public static IWorkflow If(this IWorkflow workflow, Func<bool> condition, IActivity then, IActivity @else)
        {
            workflow.Then(new If(condition, then, @else));
            return workflow;
        }
    }
}
