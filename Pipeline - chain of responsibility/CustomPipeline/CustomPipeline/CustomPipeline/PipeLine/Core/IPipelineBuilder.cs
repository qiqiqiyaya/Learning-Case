using System;
using System.Threading.Tasks;

namespace CustomPipeline.PipeLine.Core
{
    /// <summary>
    /// Represents a builder to build pipeline with registered pipes.
    /// </summary>
    /// <typeparam name="TContext">The type of the pipeline execution context.</typeparam>
    public interface IPipelineBuilder<TContext>
    {
        internal string PipelineName { get; }

        public IPipelineBuilder<TContext> Use<TPipe>() where TPipe : Pipe<TContext>;

        internal Func<TContext, ValueTask> Build(IServiceProvider serviceProvider, out PipeDescriptorInfo exportedPipeline);
    }
}
