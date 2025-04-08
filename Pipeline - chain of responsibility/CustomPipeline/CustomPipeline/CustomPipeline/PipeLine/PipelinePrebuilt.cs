using CustomPipeline.PipeLine.Core;

namespace CustomPipeline.PipeLine
{
    /// <summary>
    /// pipeline 预构
    /// </summary>
    public abstract class PipelinePrebuilt<TContext>
    {
        protected PipelinePrebuilt() { }

        public abstract IPipelineBuilder<TContext> Build(IPipelineBuilderFactory factory);
    }
}
