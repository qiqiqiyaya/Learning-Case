using Autofac;

namespace SimplePipeline.Core
{
    public class PipelineContext
    {
        public PipelineContext(ILifetimeScope serviceProvider, Employee employee)
        {
            ServiceProvider = serviceProvider;
            Employee = employee;
            DynamicProperties = new Dictionary<string, object>();
            ExecutingLog = new List<string>();
        }

        public Dictionary<string, object> DynamicProperties { get; set; }

        public ILifetimeScope ServiceProvider { get; set; }

        public List<string> ExecutingLog { get; protected set; }

        /// <summary>
        /// input
        /// </summary>
        public Employee Employee { get; set; }

        public void AddExecutingLog(string log)
        {
            ExecutingLog.Add(log);
        }
    }
}
