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
SetCache(app.Services.GetService<IDistributedCache>());

app.UseMiddleware<RedisReaderMiddleware>();
app.Run();


void SetCache(IDistributedCache? cache)
{
    if (cache == null) throw new NullReferenceException(nameof(IDistributedCache) + " is null.");
    cache.SetString(CacheKeys.Test1, "12321312321321321");
    cache.SetString(CacheKeys.Test2, "22222222222222222222");
}