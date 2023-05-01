using Autofac;
using SimplePipeline.Core;

namespace SimplePipeline.BusinessTest
{
    public class SalaryPipelineContext : IPipelineContext
    {
        public SalaryPipelineContext(ILifetimeScope serviceProvider)
        {
            ServiceProvider = serviceProvider;
            DynamicProperties = new Dictionary<string, object>();
        }

        public Dictionary<string, object> DynamicProperties { get; set; }

        public ILifetimeScope ServiceProvider { get; set; }

        /// <summary>
        /// Executing Log
        /// </summary>
        public List<string> ExecutingLog { get; set; } = new List<string>();
    }
}
