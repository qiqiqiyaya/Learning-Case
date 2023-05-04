using SimplePipeline.Core;
using SimplePipeline.Rule;
using SimplePipeline.Rule.Workflow;

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


            // 解析具体规则
            var trees = context.GetRuleTrees();
            

            Console.WriteLine(context.Subject.Name);
        }
    }
}
