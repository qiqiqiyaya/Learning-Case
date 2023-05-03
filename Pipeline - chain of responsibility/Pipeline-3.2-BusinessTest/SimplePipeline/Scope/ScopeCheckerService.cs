using Newtonsoft.Json;
using SimplePipeline.Core;

namespace SimplePipeline.Scope
{
    public class ScopeCheckerService : IScopeCheckerService
    {
        public Task<ScopeCheckResult> CheckAsync(Subject subject, Employee employee)
        {
            // check
            foreach (var scope in subject.Scopes)
            {
                if (scope.IsSatisfy(employee))
                {
                    return Task.FromResult(ScopeCheckResult.Success);
                }
            }

            string log = FailedLog(subject, employee);

            return Task.FromResult(new ScopeCheckResult() { Successed = false, CheckLog = log });
        }

        protected virtual string FailedLog(Subject subject, Employee employee)
        {
            return $"{subject.Name} , value: {JsonConvert.SerializeObject(subject.Scopes)} ,employee: {employee.Code}";
        }
    }
}
