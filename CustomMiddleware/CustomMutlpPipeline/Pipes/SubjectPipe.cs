namespace CustomMutlpPipeline.Pipes
{
    class SubjectPipe : Pipe
    {
        public override ValueTask InvokeAsync(PipelineContext context, Func<PipelineContext, ValueTask> next)
        {
            Console.WriteLine("SubjectPipe");
            return next(context);
        }
    }
}
