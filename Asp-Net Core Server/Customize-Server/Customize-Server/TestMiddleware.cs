namespace Customize_Server
{
    public class TestMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("TestMiddleware \r\n");
            await next(context);
        }
    }
}
