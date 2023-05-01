namespace SimplePipeline.Core
{
    /// <summary>
    /// Handler Map
    /// </summary>
    public class HandlerMap<TData>
    {
        /// <summary>
        /// Handler
        /// </summary>
        public IHandler<IHandlerExecutionContext> Handler { get; set; }

        /// <summary>
        /// data
        /// </summary>
        public TData Data { get; set; }

        public HandlerMap(IHandler<IHandlerExecutionContext> handler, TData data)
        {
            Handler = handler;
            Data = data;
        }
    }
}
