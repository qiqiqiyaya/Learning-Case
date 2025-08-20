using ConsoleApp1.Interceptors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Modularity;

namespace ConsoleApp1
{
    [DependsOn(typeof(AbpAuditingModule))]
    public class TestAuditModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.OnRegistered(options =>
            {
                if (options.ImplementationType.IsDefined(typeof(TestAttribute), true))
                {
                    options.Interceptors.TryAdd<TestInterceptor>();
                }
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddTransient<IOrderingService, OrderingService>();
            context.Services.AddTransient<IEmployeeTestService, EmployeeTestService>();

            context.Services.Replace(ServiceDescriptor.Singleton<IAuditingStore, TestAuditService>());

            Configure<AbpAuditingOptions>(options =>
            {
                options.IsEnabled = true;
                options.ApplicationName = "ConsoleApp1";
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {

        }
    }
}
