
// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using RedisCacheUsing;
using StackExchange.Redis;

var serviceCollection = new ServiceCollection();
// Redis connection string
ConfigurationOptions configuration = ConfigurationOptions.Parse("127.0.0.1:5005,password=123456,abortConnect=false");
ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(configuration);
serviceCollection.AddSingleton(connectionMultiplexer);
serviceCollection.AddSingleton<StackExchangeRedis>();
var serviceProvider = serviceCollection.BuildServiceProvider();


var service = serviceProvider.GetService<StackExchangeRedis>();


// string
await service.StringSetAsync(CacheKeys.StringTest1, "23121312321312");
Write(CacheKeys.StringTest1, "write 23121312321312");
await service.StringAppendAsync(CacheKeys.StringTest1, "55555");
Write(CacheKeys.StringTest1, "Append 55555");
var str = await service.StringGetAsync(CacheKeys.StringTest1);
Write(CacheKeys.StringTest1, "Value " + str);
var count = await service.StringBitCountAsync(CacheKeys.StringTest1);
Write(CacheKeys.StringTest1, "count " + count);

var range = await service.StringGetRangeAsync(CacheKeys.StringTest1,3,5);
Write(CacheKeys.StringTest1, "range " + range);

Console.WriteLine(" ");
Console.WriteLine(" ");
Console.WriteLine(" hash ");

// hash
// 如果hash中设置重复键，后者将会覆盖
var profile = new Dictionary<string, string>();
profile.Add("age","22");
profile.Add("name", "hash");
profile.Add("health", "good");
profile.Add("float", "33");
await service.HashSetAsync(CacheKeys.HashTest1, profile.ToHashEntries());
var aa = await service.HashGetAllAsync(CacheKeys.HashTest1);
foreach (var entry in aa)
{
    WriteMsg(CacheKeys.HashTest1, entry.Name+ ": " + entry.Value);
}
//对hash中的field值减1
var a = await service.HashDecrementAsync(CacheKeys.HashTest1, "float", 1);
Write(CacheKeys.HashTest1, a.ToString());
var lease = await service.HashGetLeaseAsync(CacheKeys.HashTest1, "float");
WriteMsg(CacheKeys.HashTest1, "HashGetLease " + lease);

var resultAsync = service.HashScanAsync(CacheKeys.HashTest1);
await foreach (var item in resultAsync)
{
    WriteMsg(CacheKeys.HashTest1, item.Name + ": " + item.Value);
}
Console.WriteLine("Hello, World!");



static void Write(string key, string message)
{
    Console.WriteLine("key:" + key + "    message:" + message);
}

static void WriteMsg(string key, string message)
{
    Console.WriteLine("key:" + key + "  " + message);
}

