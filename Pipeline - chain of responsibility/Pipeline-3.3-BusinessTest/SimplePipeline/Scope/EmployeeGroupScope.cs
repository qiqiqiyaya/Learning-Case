using SimplePipeline.Core;

namespace SimplePipeline.Scope
{
    public class EmployeeGroupScope : Scope
    {
        public string Type { get; set; } = default!;

        public override Task<bool> IsSatisfy(Employee employee, ScopeCheckContext context)
        {
            return Task.FromResult(employee.EmployeeGroupType == Type);
        }
    }
}
