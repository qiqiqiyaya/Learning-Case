namespace SimplePipeline.Core
{
    /// <summary>
    /// pipeline
    /// </summary>
    public interface IPipeline<out TPipelineContext, TData>
        where TPipelineContext : IPipelineContext
    {
        /// <summary>
        /// all handlers
        /// </summary>
        IEnumerable<HandlerMap<TData>> HandlerMaps { get; }

        /// <summary>
        /// pipeline Context
        /// </summary>
        TPipelineContext PipelineContext { get; }

        public bool AddHeadHandler(IHeadHandler headHandler);

        /// <summary>
        /// add Handlers to pipeline
        /// </summary>
        /// <param name="handlerMaps"></param>
        /// <returns></returns>
        public bool AddHandlers(IEnumerable<HandlerMap<TData>> handlerMaps);

        /// <summary>
        /// pipeline run
        /// </summary>
        /// <returns></returns>
        Task RunAsync();
    }
}
