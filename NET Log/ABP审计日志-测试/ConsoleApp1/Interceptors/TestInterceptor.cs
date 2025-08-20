using Volo.Abp.DependencyInjection;
using Volo.Abp.DynamicProxy;

namespace ConsoleApp1.Interceptors
{
    public class TestInterceptor : AbpInterceptor, ITransientDependency
    {
        public override async Task InterceptAsync(IAbpMethodInvocation invocation)
        {

            await invocation.ProceedAsync();
        }
    }
}
