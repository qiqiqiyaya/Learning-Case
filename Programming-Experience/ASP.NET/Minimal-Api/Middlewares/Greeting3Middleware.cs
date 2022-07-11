using App.Services;

namespace App.Middlewares
{
    public class Greeting3Middleware
    {       
        // Create middleware by convention
        public Greeting3Middleware(RequestDelegate next)
        {
            // must contain a RequestDelegate parameter
        }

        // must contain an InvokeAsync or Invoke method
        // using method dependency injection 
        public async Task InvokeAsync(HttpContext context, IGreeter greeter)
        {
            await context.Response.WriteAsync(greeter.Greet(DateTimeOffset.Now));
        }
    }
}
