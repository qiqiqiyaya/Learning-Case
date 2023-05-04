using SimplePipeline.Core;

namespace SimplePipeline.Scope
{
    public class OrganizationScope : Scope
    {
        public string Code { get; set; } = default!;

        public string Name { get; set; } = default!;

        public override Task<bool> IsSatisfy(Employee employee, ScopeCheckContext context)
        {
            return Task.FromResult(employee.OrganizationCode == Code);
        }
    }
}
