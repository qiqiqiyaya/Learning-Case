namespace CustomPipeline.PipeLine.Core
{
    public static class PipelineBuilderExtensions
    {
        /// <summary>Register specified type of pipe.</summary>
        /// <typeparam name="TContext">The type of the pipeline execution context.</typeparam>
        /// <typeparam name="TPipe">The type of the pipe to register.</typeparam>
        /// <param name="pipelineBuilder">The <see cref="IPipelineBuilder{TContext}"/> to build pipeline with registered pipes.</param>
        /// <returns>The current <see cref="IPipelineBuilder{TContext}"/></returns>
        public static IPipelineBuilder<TContext> Use<TContext, TPipe>(this IPipelineBuilder<TContext> pipelineBuilder)
            where TPipe : Pipe<TContext>
        {
            if (pipelineBuilder == null)
            {
                throw new ArgumentNullException(nameof(pipelineBuilder));
            }

            return pipelineBuilder.Use<TPipe>();
        }
    }
}