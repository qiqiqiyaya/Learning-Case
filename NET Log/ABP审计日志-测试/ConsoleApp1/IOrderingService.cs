using ConsoleApp1.Interceptors;

namespace ConsoleApp1
{
    public interface IOrderingService : ITrackedLog
    {
        Task<int> Get(string id);
    }
}
