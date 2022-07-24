using Microsoft.Extensions.Caching.Distributed;
using Redis.Distributed;
using Redis.Distributed.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration.GetSection("Redis").Get<RedisConfiguration>();

builder.Services.AddDistributedRedisCache(options =>
{
    options.Configuration = config.ConnectionString;
})
    .AddSingleton<RedisReaderMiddleware>();

var app = builder.Build();

//app.UseMiddleware<RedisReaderMiddleware>();

app.MapGet("/", async p =>
 {
     await p.Response.WriteAsync("current time :" + DateTime.Now.ToLongTimeString());
 });


app.MapGet("/set", async p =>
{
    var cache = p.RequestServices.GetService<IDistributedCache>();
    SetCache(cache);
    await p.Response.WriteAsync("ok!!!");
});

app.MapGet("/get", async p =>
{
    var cache = p.RequestServices.GetService<IDistributedCache>();
    var environment = p.RequestServices.GetService<IHostEnvironment>();

    var test1 = await cache.GetStringAsync(CacheKeys.Test1);
    var test2 = await cache.GetStringAsync(CacheKeys.Test2);
    await p.Response.WriteAsync("test1:" + test1 + "<br/>test2" + test2 + "<br/>environment:" + environment.EnvironmentName);
});
app.Run();

async void SetCache(IDistributedCache? cache)
{
    await cache.SetStringAsync(CacheKeys.Test1, "12321312321321321");
    await cache.SetStringAsync(CacheKeys.Test2, "22222222222222222222");
}