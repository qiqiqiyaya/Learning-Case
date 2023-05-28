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

        public static DisposeAction BeginNewScope(Action<IServiceProvider> action)
        {
            var serviceProvider = _serviceCollection.BuildServiceProvider();
            action(serviceProvider);
            return new DisposeAction(() =>
            {
                serviceProvider.Dispose();
            });
        }
    }
}
