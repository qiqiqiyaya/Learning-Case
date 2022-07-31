using Customize_Router.Data;
using Customize_Router.Endpoints.Results;

namespace Customize_Router.Endpoints
{
    public class ApiGetListEndpoint : IEndpoint
    {
        private readonly ILogger<ApiGetListEndpoint> _logger;
        private readonly DataContext _dataContext;

        public ApiGetListEndpoint(ILogger<ApiGetListEndpoint> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        public Task<IEndpointResult> ProcessAsync(HttpContext context)
        {
            if (!HttpMethods.IsGet(context.Request.Method))
            {
                _logger.LogWarning("Invalid HTTP request for ApiGetList");
                return Task.FromResult<IEndpointResult>(new ErrorResult("Invalid HTTP request for ApiGetList"));
            }


            var api = _dataContext.GetList();

            return Task.FromResult<IEndpointResult>(new SuccessResult<List<Api>>(api));
        }
    }
}
