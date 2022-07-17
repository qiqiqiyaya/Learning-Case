using Microsoft.Extensions.Caching.Distributed;

namespace Redis.Distributed.Middlewares
{
    public class RedisReaderMiddleware : IMiddleware
    {
        private readonly IDistributedCache _cache;
        private readonly IHostEnvironment _environment;

        public RedisReaderMiddleware(IDistributedCache cache, IHostEnvironment environment)
        {
            _cache = cache;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var test1 = await _cache.GetStringAsync(CacheKeys.Test1);
            var test2 = await _cache.GetStringAsync(CacheKeys.Test2);
            await context.Response.WriteAsync("test1:" + test1 + "<br/>test2" + test2 + "<br/>environment:" + _environment.EnvironmentName);
        }
    }
}
