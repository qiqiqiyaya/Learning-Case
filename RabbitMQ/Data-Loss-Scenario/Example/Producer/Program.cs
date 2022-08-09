using Newtonsoft.Json;
using Producer;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IMessageProducer, RabbitMqProducer>();
var app = builder.Build();

app.MapGet("/", async context =>
 {
     var producer = context.RequestServices.GetRequiredService<IMessageProducer>();
     var host = context.RequestServices.GetRequiredService<IHostEnvironment>();

     var msg = new Message(host.ApplicationName, host.ContentRootPath, host.EnvironmentName);
     producer.SendMessage(msg);
     await context.Response.WriteAsync(JsonConvert.SerializeObject(msg));
 });

app.Run();
