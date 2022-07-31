using Customize_Router;
using Customize_Router.Data;
using Customize_Router.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<CustomizeRouterMiddleware>();
builder.Services.AddSingleton<DataContext>();
builder.Services.AddCustomizeRouter();
builder.Services.AddRazorPages();

var app = builder.Build();
app.UseMiddleware<CustomizeRouterMiddleware>();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.MapGet("/", context => context.Response.WriteAsync("):"));
app.Run();
