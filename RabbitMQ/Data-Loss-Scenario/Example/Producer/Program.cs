using Newtonsoft.Json;
using Producer;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<Transaction>();
builder.Services.AddSingleton<GeneralConfirm>();
builder.Services.AddSingleton<BatchConfirm>();
builder.Services.AddSingleton<AsyncConfirm>();
var app = builder.Build();

app.MapGet("/Transaction", async context =>
 {
     var producer = context.RequestServices.GetRequiredService<Transaction>();
     var host = context.RequestServices.GetRequiredService<IHostEnvironment>();

     var msg = new Message(host.ApplicationName, host.ContentRootPath, host.EnvironmentName);
     producer.SendMessage(msg);
     await context.Response.WriteAsync(JsonConvert.SerializeObject(msg));
 });

app.MapGet("/GeneralConfirm", async context =>
{
    var producer = context.RequestServices.GetRequiredService<GeneralConfirm>();
    var host = context.RequestServices.GetRequiredService<IHostEnvironment>();

    var msg = new Message(host.ApplicationName, host.ContentRootPath, host.EnvironmentName);
    var result = producer.SendMessage(msg);
    await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
});


app.MapGet("/BatchConfirm", async context =>
{
    var producer = context.RequestServices.GetRequiredService<BatchConfirm>();
    var host = context.RequestServices.GetRequiredService<IHostEnvironment>();

    var msg = new Message(host.ApplicationName, host.ContentRootPath, host.EnvironmentName);
    var result = producer.SendMessage(msg);
    await context.Response.WriteAsync(result.ToString());
});


app.MapGet("/AsyncConfirm", async context =>
{
    var producer = context.RequestServices.GetRequiredService<AsyncConfirm>();
    var host = context.RequestServices.GetRequiredService<IHostEnvironment>();

    var msg = new Message(host.ApplicationName, host.ContentRootPath, host.EnvironmentName);
    producer.SendMessage(msg);
    await context.Response.WriteAsync("ok");
});
app.Run();
