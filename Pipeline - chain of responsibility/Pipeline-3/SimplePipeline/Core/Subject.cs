using Bogus;

namespace SimplePipeline.Core
{
    public class Subject
    {
        public string Name { get; set; } = default!;

        public static List<Subject> TestData()
        {
            Randomizer.Seed = new Random(8675309);

            var test = new Faker<Subject>()
                .StrictMode(true)
                .RuleFor(s => s.Name, f => f.Name.FullName());

            var data = test.Generate(10);
            return data;
        }
    }


}
