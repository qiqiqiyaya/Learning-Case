using SimplePipeline.Core;

namespace SimplePipeline.Scope
{
    public class EmployeeScope : Scope
    {
        public string Code { get; set; } = default!;

        public override Task<bool> IsSatisfy(Employee employee, ScopeCheckContext context)
        {
            return Task.FromResult(employee.Code == Code);
        }
    }
}
