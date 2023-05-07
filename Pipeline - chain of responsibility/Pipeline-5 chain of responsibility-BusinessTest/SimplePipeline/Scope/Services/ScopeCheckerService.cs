using Newtonsoft.Json;
using SimplePipeline.Core;

namespace SimplePipeline.Scope.Services
{
    public class ScopeCheckerService : IScopeCheckerService
    {
        public async Task<ScopeCheckResult> CheckAsync(ScopeCheckContext context)
        {
            var scopes = context.Scopes;
            var employee = context.Employee;

            // check
            foreach (var scope in scopes)
            {
                var isSatisfy = await scope.IsSatisfy(employee, context);
                // 不满足条件， 返回false ，向下执行
                if (!isSatisfy)
                {
                    context.AddFailedLog(scope);
                    continue;
                }

                // 满足条件，且享有 返回true
                if (scope.Benefit && isSatisfy)
                {
                    // TODO 输出成功的日志 
                    context.AddSuccessedLog(scope);
                    return ScopeCheckResult.Success;
                }

                // 满足条件，且不享有 返回 false
                if (!scope.Benefit && isSatisfy)
                {
                    // 继续往下执行
                    context.AddFailedLog(scope);
                    continue;
                }
            }

            string log = FailedLog(context.Subject, employee);

            // "所有条件不满足， 返回 false"
            return new ScopeCheckResult() { Successed = false, CheckLog = log };
        }
        
        protected virtual string FailedLog(Subject subject, Employee employee)
        {
            return $"{subject.Name} , value: {JsonConvert.SerializeObject(subject.Scopes)} ,employee: {employee.Code}";
        }
    }
}
