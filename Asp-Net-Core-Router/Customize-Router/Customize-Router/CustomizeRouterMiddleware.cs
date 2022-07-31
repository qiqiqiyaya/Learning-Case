using Customize_Router.Endpoints;

namespace Customize_Router
{
    public class CustomizeRouterMiddleware : IMiddleware
    {
        private readonly ILogger<CustomizeRouterMiddleware> _logger;
        private readonly EndpointConfiguration _configuration;

        public CustomizeRouterMiddleware(ILogger<CustomizeRouterMiddleware> logger, EndpointConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var endPoint = FindRouter(context);

            if (endPoint != null)
            {
                var result = await endPoint.ProcessAsync(context);
                await result.ExecuteAsync(context);
                return;
            }

            await next(context);
        }

        public IEndpoint? FindRouter(HttpContext context)
        {
            foreach (var pair in _configuration.Endpoints)
            {
                var path = pair.Key;
                if (context.Request.Path.Equals(path, StringComparison.OrdinalIgnoreCase))
                {
                    var obj = context.RequestServices.GetRequiredService(pair.Value);

                    return obj as IEndpoint;
                }
            }

            return null;
        }
    }
}
