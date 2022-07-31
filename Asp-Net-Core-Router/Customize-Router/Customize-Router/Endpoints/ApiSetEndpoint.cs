using System.ComponentModel.DataAnnotations;
using Customize_Router.Data;
using Customize_Router.Endpoints.Results;

namespace Customize_Router.Endpoints
{
    public class ApiSetEndpoint : IEndpoint
    {
        private readonly ILogger<ApiSetEndpoint> _logger;
        private readonly DataContext _dataContext;

        public ApiSetEndpoint(ILogger<ApiSetEndpoint> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        public async Task<IEndpointResult> ProcessAsync(HttpContext context)
        {
            if (!HttpMethods.IsPost(context.Request.Method))
            {
                _logger.LogWarning("Invalid HTTP request for ApiGetList");
                return await Task.FromResult<IEndpointResult>(new ErrorResult("Invalid HTTP request for ApiGetList"));
            }

            var api = await context.Request.ReadFromJsonAsync<Api>();
            if (api == null)
            {
                return await Task.FromResult<IEndpointResult>(new ErrorResult("Setting failed"));
            }

            List<ValidationResult> validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(api);
            if (!Validator.TryValidateObject(api, validationContext, validationResults))
            {
                return await Task.FromResult<IEndpointResult>(new ErrorResult<List<ValidationResult>>(validationResults));
            }

            _dataContext.Set(api);

            return await Task.FromResult<IEndpointResult>(new SuccessResult<string>());
        }
    }
}
