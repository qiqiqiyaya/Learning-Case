using SimplePipeline.Core;

namespace SimplePipeline
{
    public class SubjectHandler : IHandler
    {
        public Task ExecuteAsync(HandlerExecutionContext context)
        {
            Console.WriteLine(context.Subject.Name);
            return  Task.CompletedTask;
        }
    }
}
