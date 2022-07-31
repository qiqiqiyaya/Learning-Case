using Customize_Router.Endpoints.Results;

namespace Customize_Router.Endpoints
{
    public interface IEndpoint
    {
        Task<IEndpointResult> ProcessAsync(HttpContext context);
    }
}
