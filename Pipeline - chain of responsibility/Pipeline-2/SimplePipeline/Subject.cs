using SimplePipeline.Core;

namespace SimplePipeline
{
    public class Subject : IStep
    {
        public Task ExecuteAsync(SubjectExecutionContext context)
        {
            Console.WriteLine(context.SubjectData.Name);
            return  Task.CompletedTask;
        }
    }
}
