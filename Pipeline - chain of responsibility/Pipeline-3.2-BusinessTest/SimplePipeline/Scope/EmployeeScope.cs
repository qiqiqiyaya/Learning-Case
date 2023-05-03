namespace SimplePipeline.Scope
{
    public class EmployeeScope : Scope
    {
        public string Code { get; set; } = default!;

        public override bool IsSatisfy(Employee employee)
        {
            return employee.Code == Code;
        }
    }
}
