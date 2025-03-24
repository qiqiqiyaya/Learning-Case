namespace CustomMiddleware
{
    public class Pipeline
    {
        private readonly Func<PipelineContext, ValueTask> _pipeline;

        public Pipeline(Func<PipelineContext, ValueTask> pipeline)
        {
            _pipeline = pipeline;
        }

        public ValueTask ExecuteAsync(PipelineContext context)
        {
            return _pipeline(context);
        }
    }
}
