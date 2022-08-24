using Microsoft.AspNetCore.Routing.Patterns;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();
app.MapGet("/", () => "Hello World!");


app.UseEndpoints(config =>
{
    // RouteEndpointBuilder
    var pattern = RoutePatternFactory.Parse("get");
    var endpointBuilder = new RouteEndpointBuilder(
        async context =>
        {
            var aa = context.GetEndpoint();

            await context.Response.WriteAsync("test");
            await Task.CompletedTask;
        },
        RoutePatternFactory.Parse("get"),
        0)
    {
        DisplayName = pattern.RawText ?? pattern.ToString(),
        Metadata = { new HttpMethodMetadata(new[] { "POST" }) }
    };

    config.DataSources.Add(new DefaultEndpointDataSource(endpointBuilder.Build()));
});


app.Run();
