using Customize_Router.Endpoints.Results;

namespace Customize_Router.Endpoints
{
    public class ApiSetEndpoint : IEndpoint
    {
        private readonly Logger<ApiGetEndpoint> _logger;

        public ApiSetEndpoint(Logger<ApiGetEndpoint> logger)
        {
            _logger = logger;
        }

        public Task<IEndpointResult> ProcessAsync(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}
