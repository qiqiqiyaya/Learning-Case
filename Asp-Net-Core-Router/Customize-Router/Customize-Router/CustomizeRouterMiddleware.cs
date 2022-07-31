namespace Customize_Router
{
    public class CustomizeRouterMiddleware : IMiddleware
    {
        private readonly ILogger<CustomizeRouterMiddleware> _logger;

        public CustomizeRouterMiddleware(ILogger<CustomizeRouterMiddleware> logger)
        {
            _logger = logger;
        }

        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            throw new NotImplementedException();
        }
    }
}
