namespace CustomMutlpPipeline
{
    public class PipelineBuilder : IPipelineBuilder
    {
        private readonly List<Pipe> _pipes = new List<Pipe>();

        private PipelineBuilder(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        /// <summary>
        /// The current service provider to resolve services from.
        /// </summary>
        public IServiceProvider ServiceProvider { get; }

        public PipelineBuilder Use(Pipe pipe)
        {
            _pipes.Add(pipe);
            return this;
        }

        public Pipeline Build(string name)
        {
            Func<PipelineContext, ValueTask> nextPipeline = _ => new ValueTask();
            for (int index = _pipes.Count - 1; index > -1; index--)
            {
                var pipe = _pipes[index];
                var convertedPipe = Convert(pipe);
                nextPipeline = convertedPipe(nextPipeline);
            }

            return new Pipeline(nextPipeline, name);
        }

        private static Func<Func<PipelineContext, ValueTask>, Func<PipelineContext, ValueTask>> Convert(Pipe pipe)
        {
            return next => context => pipe.InvokeAsync(context, next);
        }

        public static PipelineBuilder Create(IServiceProvider serviceProvider)
        {
            return new PipelineBuilder(serviceProvider);
        }
    }
}
