using Customize_Router.Data;
using Customize_Router.Endpoints.Results;

namespace Customize_Router.Endpoints
{
    public class ApiGetEndpoint : IEndpoint
    {
        private readonly Logger<ApiGetEndpoint> _logger;
        private readonly DataContext _dataContext;

        public ApiGetEndpoint(Logger<ApiGetEndpoint> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        public Task<IEndpointResult> ProcessAsync(HttpContext context)
        {
            if (HttpMethods.IsGet(context.Request.Method))
            {
                _logger.LogWarning("Invalid HTTP request for ApiGet");
                return Task.FromResult<IEndpointResult>(new ErrorResult("Invalid HTTP request for ApiGet"));
            }

            if (!context.Request.Query.ContainsKey("name"))
            {
                return Task.FromResult<IEndpointResult>(new ErrorResult("No name parameter"));
            }

            var name = context.Request.Query["name"];
            var api = _dataContext.Get(name);

            return Task.FromResult<IEndpointResult>(new SuccessResult<Api>(api));
        }
    }
}
