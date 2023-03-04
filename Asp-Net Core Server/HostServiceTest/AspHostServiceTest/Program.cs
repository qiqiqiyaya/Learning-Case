using AspHostServiceTest;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHostedService<TestHostService>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
