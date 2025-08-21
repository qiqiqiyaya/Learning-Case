using ConsoleApp1.Interceptors;

namespace ConsoleApp1
{
    public interface IEmployeeTestService : ITrackedLog
    {
        Task GetEmployeeAsync();
    }
}
