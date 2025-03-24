namespace CustomMutlpPipeline
{
    public interface IMiddleware
    {
        ValueTask InvokeAsync(PipelineContext context);
    }
}
