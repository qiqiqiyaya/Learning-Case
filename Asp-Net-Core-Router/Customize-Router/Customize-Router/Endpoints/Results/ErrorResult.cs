using System.Text.Json;

namespace Customize_Router.Endpoints.Results
{
    public class ErrorResult : ErrorResult<string>
    {
        public ErrorResult(string msg)
            : base(msg)
        {

        }

        public override async Task ExecuteAsync(HttpContext context)
        {
            await context.Response.WriteAsync(Data);
        }
    }

    public class ErrorResult<T> : IEndpointResult
    {
        protected readonly T Data;

        public ErrorResult(T data)
        {
            Data = data;
        }

        public virtual async Task ExecuteAsync(HttpContext context)
        {
            await context.Response.WriteAsync(JsonSerializer.Serialize(Data));
        }
    }
}
