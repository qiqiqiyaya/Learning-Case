namespace CustomMutlpPipeline
{
    public abstract class Pipe
    {
        public abstract ValueTask InvokeAsync(PipelineContext context, Func<PipelineContext, ValueTask> next);
    }
}
