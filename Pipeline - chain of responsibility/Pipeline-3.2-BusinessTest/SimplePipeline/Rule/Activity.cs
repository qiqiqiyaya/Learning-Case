using SimplePipeline.Core;

namespace SimplePipeline.Rule
{
    public abstract class Activity : IActivity
    {
        public int Id { get; set; }

        public virtual Task ExecuteAsync(HandlerExecutionContext context)
        {
            return Task.CompletedTask;
        }
    }
}
