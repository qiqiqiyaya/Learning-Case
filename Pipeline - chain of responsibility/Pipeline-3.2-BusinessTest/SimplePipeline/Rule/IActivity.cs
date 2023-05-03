using SimplePipeline.Core;

namespace SimplePipeline.Rule
{
    public interface IActivity
    {
        public int Id { get; set; }

        public Task ExecuteAsync(HandlerExecutionContext context);
    }
}
