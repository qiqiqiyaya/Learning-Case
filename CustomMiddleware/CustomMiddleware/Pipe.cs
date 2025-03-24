namespace CustomMiddleware
{
    public abstract class Pipe
    {
        public abstract ValueTask InvokeAsync(PipelineContext context, Func<PipelineContext, ValueTask> next);
    }
}
