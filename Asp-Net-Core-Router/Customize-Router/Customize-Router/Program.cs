using Customize_Router;
using Customize_Router.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<CustomizeRouterMiddleware>();
builder.Services.AddSingleton<DataContext>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseRouting();

app.Run();
