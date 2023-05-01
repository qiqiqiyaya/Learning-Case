using SimplePipeline.Core;

namespace SimplePipeline.Extensions
{
    public class HeadHandler : IHeadHandler
    {
        public Task ExecuteAsync(HeadHandlerExecutionContext context)
        {

            return Task.CompletedTask;
        }
    }
}
