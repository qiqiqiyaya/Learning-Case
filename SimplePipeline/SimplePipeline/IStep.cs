namespace SimplePipeline
{
    public interface IStep<T> 
        where T : IDataContext
    {
        Task ExecuteAsync(T context);
    }
}
