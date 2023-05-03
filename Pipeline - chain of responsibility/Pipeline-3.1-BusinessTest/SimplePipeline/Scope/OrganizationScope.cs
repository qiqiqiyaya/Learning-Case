namespace SimplePipeline.Scope
{
    public class OrganizationScope : Scope
    {
        public string Code { get; set; } = default!;

        public string Name { get; set; } = default!;

        public override bool IsSatisfy(Employee employee)
        {
            return employee.OrganizationCode == Code;
        }
    }
}
