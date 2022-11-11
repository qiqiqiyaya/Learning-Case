using App.Services;

namespace App.Middlewares
{
    public class Greeting2Middleware
    {
        // Create middleware by convention

        private readonly IGreeter _greeter;
        public Greeting2Middleware(IGreeter greeter,RequestDelegate next)
        {
            // must contain a RequestDelegate parameter
            _greeter = greeter;
        }

        // must contain an InvokeAsync or Invoke method
        public async Task InvokeAsync(HttpContext context)
        {
            await context.Response.WriteAsync(_greeter.Greet(DateTimeOffset.Now));
        }
    }
}
