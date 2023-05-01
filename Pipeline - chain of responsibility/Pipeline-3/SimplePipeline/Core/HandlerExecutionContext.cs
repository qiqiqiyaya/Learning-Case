using Autofac;

namespace SimplePipeline.Core
{
    /// <summary>
    /// subject executing context
    /// </summary>
    public class HandlerExecutionContext
    {
        public HandlerExecutionContext(PipelineContext pipelineContext, Subject subject)
        {
            PipelineContext = pipelineContext;
            Subject = subject;
        }

        public PipelineContext PipelineContext { get; }

        public ILifetimeScope ServiceProvider => PipelineContext.ServiceProvider;

        public Subject Subject { get; }

        public T GetRequiredService<T>()
            where T : notnull
        {
            return ServiceProvider.Resolve<T>();
        }
    }
}