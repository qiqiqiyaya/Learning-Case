using Autofac;

namespace SimplePipeline.Core
{
    public class BasePipeLineContext : IPipelineContext
    {
        public BasePipeLineContext(ILifetimeScope serviceProvider)
        {
            ServiceProvider = serviceProvider;
            DynamicProperties = new Dictionary<string, object>();
        }

        public Dictionary<string, object> DynamicProperties { get; set; }

        public ILifetimeScope ServiceProvider { get; set; }

        /// <summary>
        /// executing log
        /// </summary>
        public List<string> ExecutingLog { get; set; } = new List<string>();
    }
}
