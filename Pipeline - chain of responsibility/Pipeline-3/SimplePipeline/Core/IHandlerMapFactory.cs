namespace SimplePipeline.Core
{
    /// <summary>
    /// Handler Map Factory
    /// </summary>
    public interface IHandlerMapFactory<TData>
    {
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        IEnumerable<HandlerMap<TData>> Create(List<Subject> data);
    }
}
