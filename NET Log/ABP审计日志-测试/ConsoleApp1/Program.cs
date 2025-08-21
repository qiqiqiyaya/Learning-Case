using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Auditing;

namespace ConsoleApp1
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var app = AbpApplicationFactory.Create<TestAuditModule>(options =>
            {
                options.UseAutofac();
            });
            app.Initialize();
            var serviceProvider = app.CreateServiceProvider();

            var auditManager = serviceProvider.GetRequiredService<IAuditingManager>();

            using (var auditLog = auditManager.BeginScope())
            {
                var service = serviceProvider.GetRequiredService<IEmployeeTestService>();
                await service.GetEmployeeAsync();

                await auditLog.SaveAsync();
            }

            Console.ReadLine();
        }
    }
}
