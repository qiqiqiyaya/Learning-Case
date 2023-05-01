namespace SimplePipeline.Core
{
    /// <summary>
    /// handler
    /// </summary>
    public interface IHandler<in THandlerExecutionContext>
        where THandlerExecutionContext : IHandlerExecutionContext
    {
        /// <summary>
        /// Execute
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task ExecuteAsync(THandlerExecutionContext context);
    }
}
