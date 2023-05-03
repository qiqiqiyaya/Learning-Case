namespace SimplePipeline.Scope
{
    public class EmployeeGroupScope : Scope
    {
        public string Type { get; set; } = default!;

        public override bool IsSatisfy(Employee employee)
        {
            return employee.EmployeeGroupType == Type;
        }
    }
}
