using SimplePipeline.Core;
using SimplePipeline.Rule;

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

            IWorkflow workflow = new Workflow();

            workflow.If(async executionContext => await executionContext.Or(typeof(EmployeeGroupRule), typeof(ProbationPeriodRule)),
                new Workflow(new List<IActivity>()
                {
                    new If(executionContext => Task.FromResult(22 > 0),
                        new FunctionActivity(executionContext =>
                        {
                            Console.WriteLine("111");
                            return Task.CompletedTask;
                        }),
                        new FunctionActivity(executionContext =>
                        {
                            Console.WriteLine("222");
                            return Task.CompletedTask;
                        }))
                }),
                new FunctionActivity(executionContext =>
                {
                    Console.WriteLine("3333");
                    return Task.CompletedTask;
                }));

            await workflow.Run(context);



            Console.WriteLine(context.Subject.Name);
        }
    }
}
