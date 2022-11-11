
//RequestDelegate handler = context => context.Response.WriteAsync("Hello,World!");
//WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
//WebApplication app = builder.Build();
//app.Run(handler);
//app.Run();


// 1.2.3 Middleware
//var app = WebApplication.Create(args);
//IApplicationBuilder builder = app;
//builder.Use(HelloMiddleware)
//    .Use(WorldMiddleware);
//app.Run();

//static RequestDelegate HelloMiddleware(RequestDelegate next) =>
//    async httpcontext =>
//    {
//        await httpcontext.Response.WriteAsync("Hello,");
//        await next(httpcontext);
//    };

//static RequestDelegate WorldMiddleware(RequestDelegate next) =>
//    httpcontext => httpcontext.Response.WriteAsync("World!");



//var app = WebApplication.Create(args);
//IApplicationBuilder builder = app;
//builder.Use(HelloMiddleware)
//    .Use(WorldMiddleware);
//app.Run();

////Func<HttpContext,RequestDelegate,Task>
////Func<HttpContext,Func<Task>,Task>
//static async Task HelloMiddleware(HttpContext context, RequestDelegate next)
//{
//    await context.Response.WriteAsync("Hello,");
//    await next(context);
//}

//static Task WorldMiddleware(HttpContext context, RequestDelegate next) =>
//    context.Response.WriteAsync("World!");


// IMiddleware 中间件

//using App.Middlewares;
//using App.Services;

//var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddSingleton<IGreeter, Greeter>()
//    .AddSingleton<GreetingMiddleware>();

//var app= builder.Build();
//app.UseMiddleware<GreetingMiddleware>();
//app.Run();



// convention middleware
// Create middleware by convention
//using App.Middlewares;
//using App.Services;

//var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddSingleton<IGreeter, Greeter>();

//var app = builder.Build();
//// GreetingMiddleware will register when process app.UseMiddleware<GreetingMiddleware>() . GreetingMiddleware is a singleton service.
//app.UseMiddleware<Greeting2Middleware>();
//app.Run();



using App.Middlewares;
using App.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IGreeter, Greeter>();

var app = builder.Build();
// GreetingMiddleware will register when process app.UseMiddleware<GreetingMiddleware>() . GreetingMiddleware is a singleton service.
app.UseMiddleware<Greeting3Middleware>();
app.Run();