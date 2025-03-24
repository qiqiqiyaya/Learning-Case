namespace CustomMiddleware
{
    public class BlankPipe : Pipe
    {
        private readonly Func<PipelineContext, ValueTask> _func;

        public BlankPipe(Func<PipelineContext, ValueTask> func)
        {
            _func = func;
        }

        public override async ValueTask InvokeAsync(PipelineContext context, Func<PipelineContext, ValueTask> next)
        {
            await _func(context);
            await next(context);
        }
    }
}
