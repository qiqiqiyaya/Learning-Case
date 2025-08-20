using Volo.Abp.Auditing;

namespace ConsoleApp1
{
    internal class OrderingService : IOrderingService
    {
        public virtual Task<int> Get(string id)
        {
            return Task.FromResult(1);
        }
    }
}
