var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();
app.MapGet("/", () => "Hello World!");


//app.UseEndpoints()
app.Run();
