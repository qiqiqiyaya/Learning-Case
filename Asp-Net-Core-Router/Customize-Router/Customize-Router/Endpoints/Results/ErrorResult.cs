namespace Customize_Router.Endpoints.Results
{
    public class ErrorResult : IEndpointResult
    {
        private readonly string _msg;

        public ErrorResult(string msg)
        {
            _msg = msg;
        }

        public async Task ExecuteAsync(HttpContext context)
        {
            await context.Response.WriteAsync(_msg);
        }
    }
}
