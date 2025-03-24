namespace CustomMutlpPipeline.Pipes
{
    class TestMiddleware : IMiddleware
    {
        public ValueTask InvokeAsync(PipelineContext context)
        {
            Console.WriteLine($"{nameof(TestMiddleware)}");
            return ValueTask.CompletedTask;
        }
    }
}
