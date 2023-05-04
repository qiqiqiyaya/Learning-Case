using Bogus;
using SimplePipeline.Rule;
using SimplePipeline.Scope;

namespace SimplePipeline.Core
{
    public class Subject
    {
        public string Name { get; set; } = default!;

        public List<Scope.Scope> Scopes { get; set; } = new List<Scope.Scope>();

        public List<OriginalRule> Rules { get; set; } = new List<OriginalRule>();

        public static List<Subject> TestData()
        {
            Randomizer.Seed = new Random(8675309);

            var test = new Faker<Subject>()
                .StrictMode(true)
                .RuleFor(s => s.Name, f => f.Name.FullName())
                .RuleFor(s => s.Scopes, f => new List<Scope.Scope>()
                {
                    new EmployeeGroupScope() { Id = f.IndexFaker, Type = f.UniqueIndex.ToString() },
                    new EmployeeScope() { Id = f.IndexFaker, Code = f.Phone.Locale },
                    new OrganizationScope() { Id = f.IndexFaker, Code = f.UniqueIndex.ToString(), Name = f.Name.Locale }
                });

            var data = test.Generate(10);
            return data;
        }
    }


}
