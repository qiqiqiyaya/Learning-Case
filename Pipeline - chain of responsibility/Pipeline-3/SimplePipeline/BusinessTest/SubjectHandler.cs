using SimplePipeline.Core;

namespace SimplePipeline.BusinessTest
{
    /// <summary>
    /// SubjectHandler
    /// </summary>
    public class SubjectHandler : IHandler<SubjectExecutionContext>
    {
        /// <inheritdoc/>
        public Task ExecuteAsync(SubjectExecutionContext context)
        {
            Console.WriteLine("233223");
            return Task.CompletedTask;
        }
    }
}
