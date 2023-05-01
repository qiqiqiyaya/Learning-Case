using Autofac;

namespace ChainOfResponsibility.Core
{
    public class PipelineContext
    {
        public PipelineContext(ILifetimeScope serviceProvider)
        {
            ServiceProvider = serviceProvider;
            DynamicProperties = new Dictionary<string, object>();
        }

        public Dictionary<string, object> DynamicProperties { get; set; }

        public ILifetimeScope ServiceProvider { get; set; }
    }
}
