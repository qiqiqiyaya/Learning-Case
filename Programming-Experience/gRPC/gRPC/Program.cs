using gRPC;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options =>
    options.ConfigureEndpointDefaults(op => op.Protocols = HttpProtocols.Http2));

builder.Services.AddGrpc();
var app = builder.Build();
app.MapGrpcService<CalculatorService>();
app.Run();
