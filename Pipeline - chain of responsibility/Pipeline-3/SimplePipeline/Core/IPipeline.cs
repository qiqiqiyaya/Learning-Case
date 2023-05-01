namespace SimplePipeline.Core
{
    public interface IPipeline
    {
        IEnumerable<HandlerMap> Maps { get; }

        public PipelineContext Context { get; }

        public bool AddHandlerMaps(IEnumerable<HandlerMap> handlerMaps);

        Task RunAsync();
    }
}
