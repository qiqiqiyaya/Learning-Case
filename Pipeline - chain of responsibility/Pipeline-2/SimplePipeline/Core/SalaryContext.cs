using Autofac;

namespace SimplePipeline.Core
{
    public class SalaryContext
    {
        public SalaryContext(ILifetimeScope serviceProvider)
        {
            ServiceProvider = serviceProvider;
            DynamicProperties = new Dictionary<string, object>();
        }

        public Dictionary<string, object> DynamicProperties { get; set; }

        public ILifetimeScope ServiceProvider { get; set; }
    }
}
