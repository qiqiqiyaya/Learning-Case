using ElectronNET.API;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseElectron(args);
builder.Services.AddElectron();

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

//app.Run();
await app.StartAsync();
// Open the Electron-Window here
await Electron.WindowManager.CreateWindowAsync();
app.WaitForShutdown();