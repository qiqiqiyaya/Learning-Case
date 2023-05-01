using Bogus;

namespace SimplePipeline.Core
{
    public class SubjectData
    {
        public string Name { get; set; } = default!;

        public static List<SubjectData> TestData()
        {
            Randomizer.Seed = new Random(8675309);

            var test = new Faker<SubjectData>()
                .StrictMode(true)
                .RuleFor(s => s.Name, f => f.Name.FullName());

            var data = test.Generate(10);
            return data;
        }
    }


}
