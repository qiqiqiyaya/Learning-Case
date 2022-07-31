using Customize_Router.Endpoints.Results;

namespace Customize_Router.Endpoints
{
    public class ApiGetListEndpoint : IEndpoint
    {
        private readonly Logger<ApiGetEndpoint> _logger;

        public ApiGetListEndpoint(Logger<ApiGetEndpoint> logger)
        {
            _logger = logger;
        }

        public Task<IEndpointResult> ProcessAsync(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}
