namespace Customize_Router.Endpoints
{
    public static class CustomizeRouterExtensions
    {
        public static IServiceCollection AddCustomizeRouter(this IServiceCollection service)
        {
            return service.AddCustomizeRouter(config =>
            {
                service.AddTransient<ApiGetEndpoint>();
                service.AddTransient<ApiGetListEndpoint>();
                service.AddTransient<ApiSetEndpoint>();

                config.AddEndpoint(EndpointConfiguration.ApiGet, typeof(ApiGetEndpoint));
                config.AddEndpoint(EndpointConfiguration.ApiGetList, typeof(ApiGetListEndpoint));
                config.AddEndpoint(EndpointConfiguration.ApiSet, typeof(ApiSetEndpoint));
            });
        }

        public static IServiceCollection AddCustomizeRouter(this IServiceCollection service, Action<EndpointConfiguration> action)
        {
            var config = new EndpointConfiguration();
            action(config);
            service.AddSingleton(config);
            return service;
        }
    }
}
