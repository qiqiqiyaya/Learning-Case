namespace CustomMiddleware
{
    public interface IMiddleware
    {
        ValueTask InvokeAsync(PipelineContext context);
    }
}
