using ConsoleApp1.Interceptors;
using Volo.Abp.Auditing;
using Volo.Abp.Data;

namespace ConsoleApp1
{
    [Test]
    internal class EmployeeTestService(IOrderingService orderingService, IAuditingManager auditingManager) : IEmployeeTestService
    {
        [Audited]
        public async Task GetEmployeeAsync()
        {
            var scope = auditingManager.Current;

            scope.Log.Comments.Add("Executed the MyService.DoItAsync method :)");
            scope.Log.SetProperty("MyCustomProperty", 42);

            await orderingService.Get("fdsfsd");
        }
    }
}
