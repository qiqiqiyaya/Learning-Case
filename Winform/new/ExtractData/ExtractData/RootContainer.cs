using Microsoft.Extensions.DependencyInjection;

namespace ExtractData
{
    internal static class RootContainer
    {
        private static ServiceCollection _serviceCollection;

        public static void SetRoot(ServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
        }

        public static void BeginNewScope(Action<IServiceProvider> action)
        {
            using (var serviceProvider = _serviceCollection.BuildServiceProvider())
            {
                action(serviceProvider);
            }
        }
    }
}
