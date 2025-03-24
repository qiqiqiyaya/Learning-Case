namespace CustomMutlpPipeline
{
    public interface IPipelineBuilder
    {
        /// <summary>
        /// The current service provider to resolve services from.
        /// </summary>
        public IServiceProvider ServiceProvider { get; }

        public PipelineBuilder Use(Pipe pipe);

        public Pipeline Build(string name);
    }
}
