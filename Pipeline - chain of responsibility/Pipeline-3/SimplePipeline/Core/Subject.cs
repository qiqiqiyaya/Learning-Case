using Bogus;

namespace SimplePipeline.Core
{
    /// <summary>
    /// Subject
    /// </summary>
    public class Subject
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; } = default!;

        /// <summary>
        /// Order
        /// </summary>
        public int Order { get; set; } 

        public static List<Subject> TestData()
        {
            Randomizer.Seed = new Random(8675309);

            var test = new Faker<Subject>()
                .StrictMode(true)
                .RuleFor(s => s.Name, f => f.Name.FullName())
                .RuleFor(s => s.Order, (f, sub) => sub.Order++);

            var data = test.Generate(10);
            return data;
        }
    }


}
