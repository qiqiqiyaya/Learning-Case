using SimplePipeline.Core;

namespace SimplePipeline
{
    public class SubjectHandler : IHandler
    {
        public async Task ExecuteAsync(HandlerExecutionContext context)
        {
            context.AddLog<SubjectHandler>($"{context.Subject.Name} - {nameof(SubjectHandler)} executing，EmployeeCode: {context.Employee.Code}");

            var result = await context.ScopeCheck();
            if (!result.Successed)
            {
                context.AddLog<SubjectHandler>(result.CheckLog);
                return;
            }



            Console.WriteLine(context.Subject.Name);
        }
    }
}
