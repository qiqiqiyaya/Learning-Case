namespace Customize_Router.Endpoints.Results
{
    public interface IEndpointResult
    {
        Task ExecuteAsync(HttpContext context);
    }
}
