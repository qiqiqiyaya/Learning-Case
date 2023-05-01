namespace ChainOfResponsibility.Core
{
    public interface IPipeline
    {
        public PipelineContext Context { get; }

        public bool AddHandler(IHandler handler);

        Task RunAsync();
    }
}
