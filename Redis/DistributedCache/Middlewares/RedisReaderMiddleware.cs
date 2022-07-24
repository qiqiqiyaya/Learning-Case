using Microsoft.Extensions.Caching.Distributed;

namespace Redis.Distributed.Middlewares
{
    public class RedisReaderMiddleware : IMiddleware
    {
        private readonly IDistributedCache _cache;
        private readonly IHostEnvironment _environment;
        private readonly ILogger<RedisReaderMiddleware> _logger;

        public RedisReaderMiddleware(IDistributedCache cache, IHostEnvironment environment,ILogger<RedisReaderMiddleware> logger)
        {
            _cache = cache;
            _environment = environment;
            _logger= logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                var test1 = await _cache.GetStringAsync(CacheKeys.Test1);
                var test2 = await _cache.GetStringAsync(CacheKeys.Test2);
                await context.Response.WriteAsync("test1:" + test1 + "<br/>test2" + test2 + "<br/>environment:" + _environment.EnvironmentName);
            }
            catch (Exception e)
            {
                await context.Response.WriteAsync(e.ToString());
            }
        }
    }
}
