using Customize_Server;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Replace(ServiceDescriptor.Singleton<IServer, HttpListenerServer>());
var app = builder.Build();

app.Run(async context =>
{
    await context.Response.WriteAsync("Hello World!");
});

var server = app.Services.GetRequiredService<IServer>();

var serverFeatures = server.Features.Get<IServerAddressesFeature>();
if (serverFeatures is null)
{
    serverFeatures = new ServerAddressesFeature();
    serverFeatures.Addresses.Add("http://localhost:5000/foobar"); // Add the default address to the IServerAddressesFeature
    server.Features.Set<IServerAddressesFeature>(serverFeatures);
}
app.Run();
