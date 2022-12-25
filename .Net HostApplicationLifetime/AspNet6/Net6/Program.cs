using System.Net;
using Serilog;
using Serilog.Events;


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .MinimumLevel.Debug()
    .WriteTo.File("log.txt")
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("App start");
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
var liftTime = app.Services.GetRequiredService<IHostApplicationLifetime>();

liftTime.ApplicationStarted.Register(() =>
{
    Log.Information("Application Started");
});

liftTime.ApplicationStopping.Register(() =>
{
    Log.Information("Application Stopping");
});

liftTime.ApplicationStopped.Register(() =>
{
    Log.Information("Application Stopped");
});

app.MapGet("/", () => "Hello World!");

app.Run();
