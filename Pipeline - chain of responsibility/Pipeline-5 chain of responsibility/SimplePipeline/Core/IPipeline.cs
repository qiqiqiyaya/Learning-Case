namespace SimplePipeline.Core
{
    public interface IPipeline
    {
        public PipelineContext Context { get; }

        public PipeLineStatus Status { get; }

        /// <summary>
        /// Pipeline initialization
        /// </summary>
        /// <param name="configuration"></param>
        void Init(PipelineConfiguration configuration);

        Task RunAsync();
    }
}
