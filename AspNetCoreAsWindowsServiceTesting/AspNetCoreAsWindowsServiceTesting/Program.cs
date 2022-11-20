

using AspNetCoreAsWindowsServiceTesting;
using System.Net;
using Serilog;
using Serilog.Events;


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .MinimumLevel.Debug()
#if DEBUG
    .WriteTo.File("log.txt")
    .WriteTo.Console()
#else
    .WriteTo.File(AppContext.BaseDirectory + "log.txt")
#endif
    .CreateBootstrapLogger();

Log.Information("App start");

var webApplicationOptions = new WebApplicationOptions()
{
    ContentRootPath = AppContext.BaseDirectory,
    Args = args,
    ApplicationName = System.Diagnostics.Process.GetCurrentProcess().ProcessName
};

try
{
    var builder = WebApplication.CreateBuilder(webApplicationOptions);
    builder.Host.UseWindowsService();

    var hotSettingConfiguration = builder.Configuration.GetSection("HostSetting").Get<HostSettingConfiguration>();
    builder.Host.UseSerilog();

    // Add services to the container.
    builder.Services.AddRazorPages();

    // If the request is not https , redirect to https port
    builder.Services.AddHttpsRedirection(options =>
    {
        options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
        options.HttpsPort = hotSettingConfiguration.Port;
    });

    builder.Services.AddHsts(options =>
    {
        options.Preload = true;
        options.IncludeSubDomains = true;
        options.MaxAge = TimeSpan.FromDays(365);
    });
    builder.Services.AddHealthChecks();

    builder.WebHost.UseKestrel(options =>
    {
        //options.Listen(new IPEndPoint(IPAddress.Any, 5066));
        //options.Listen(new IPEndPoint(IPAddress.Any, 5034));
        //options.Listen(new IPEndPoint(IPAddress.Broadcast, 5034));
        options.Listen(IPAddress.Any, hotSettingConfiguration.Port, listenOptions =>
        {
            listenOptions.UseHttps(AppContext.BaseDirectory + "test.pfx", "zhengqi");
        });
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseDeveloperExceptionPage();
    app.UseStaticFiles();

    app.UseRouting();
    app.MapHealthChecks("/healthz");
    app.UseAuthorization();

    app.Map("BaseDirectory", context =>
    {
        context.Response.WriteAsync(AppContext.BaseDirectory);
        return Task.CompletedTask;
    });
    app.MapRazorPages();
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
