using Bogus;

namespace SimplePipeline
{
    public class Employee
    {
        public string Code { get; set; } = default!;

        public string Name { get; set; } = default!;

        public string OrganizationCode { get; set; } = default!;

        public string EmployeeGroupType { get; set; } = default!;

        public static Employee TestData()
        {
            Randomizer.Seed = new Random(8675309);

            var test = new Faker<Employee>()
                .StrictMode(true)
                .RuleFor(s => s.Name, f => f.Name.FullName())
                .RuleFor(s => s.Code, f => f.IndexVariable.ToString())
                .RuleFor(s => s.OrganizationCode, f => f.Phone.Locale)
                .RuleFor(s => s.EmployeeGroupType, f => f.UniqueIndex.ToString());

            var data = test.Generate();
            return data;
        }
    }
}
