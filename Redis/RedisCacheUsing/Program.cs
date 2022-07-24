
// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using RedisCacheUsing;
using StackExchange.Redis;

var serviceCollection = new ServiceCollection();
// Redis connection string
ConfigurationOptions configuration = ConfigurationOptions.Parse("127.0.0.1:6379,password=123456,abortConnect=false");
ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(configuration);
serviceCollection.AddSingleton(connectionMultiplexer);
serviceCollection.AddSingleton<StackExchangeRedis>();
var serviceProvider = serviceCollection.BuildServiceProvider();


var service = serviceProvider.GetService<StackExchangeRedis>();


// string
//await service.StringSetAsync(CacheKeys.StringTest1, "23121312321312");
//Write(CacheKeys.StringTest1, "write 23121312321312");
//await service.StringAppendAsync(CacheKeys.StringTest1, "2222");
//Write(CacheKeys.StringTest1, "Append 55555");
//var str = await service.StringGetAsync(CacheKeys.StringTest1);
//Write(CacheKeys.StringTest1, "Value " + str);
//var count = await service.StringBitCountAsync(CacheKeys.StringTest1);
//Write(CacheKeys.StringTest1, "count " + count);

//var range = await service.StringGetRangeAsync(CacheKeys.StringTest1,3,5);
//Write(CacheKeys.StringTest1, "range " + range);

//// 指定偏移量处是0还是1
//var bit=await service.StringGetBitAsync(CacheKeys.StringTest1,3);



await service.StringSetAsync(CacheKeys.StringTest2, "1");
var str11 = await service.StringGetAsync(CacheKeys.StringTest2);
Console.WriteLine("st2: " + str11);
var bitCount = await service.StringBitCountAsync(CacheKeys.StringTest2);
Console.WriteLine("bitCount中1的数量: " + Convert.ToInt32(bitCount));

await service.StringSetBitAsync(CacheKeys.StringTest2,7,Convert.ToBoolean(0));
await service.StringSetBitAsync(CacheKeys.StringTest2, 6, Convert.ToBoolean(1));
var str22 = await service.StringGetAsync(CacheKeys.StringTest2);
Console.WriteLine("st2: " + str22);

var bi22t = await service.StringGetBitAsync(CacheKeys.StringTest2, 1);
Console.WriteLine("bit: " + Convert.ToInt32(bi22t));
var bi33t = await service.StringGetBitAsync(CacheKeys.StringTest2, 2);
Console.WriteLine("bit: " + Convert.ToInt32(bi33t));

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
profile.Add("float1", "333");
profile.Add("float2", "3233");
profile.Add("float3", "33322");
profile.Add("float4", "3333323");
await service.HashSetAsync(CacheKeys.HashTest1, profile.ToHashEntries());
var aa = await service.HashGetAllAsync(CacheKeys.HashTest1);
foreach (var entry in aa)
{
    WriteMsg(CacheKeys.HashTest1, entry.Name+ ": " + entry.Value);
}
Console.WriteLine("");
Console.WriteLine("");
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

//
Console.WriteLine("");
Console.WriteLine("HashScan  pagelist");
// 可 进行模糊搜索  *key* , 这里存在疑问，不能简单理解为分页
var pagelist = service.HashScanAsync(CacheKeys.HashTest1,"f*", 2, 0, 0);
await foreach (var item in pagelist)
{
    WriteMsg(CacheKeys.HashTest1, item.Name + ": " + item.Value);
}
Console.WriteLine(" ");
Console.WriteLine(" ");

await service.KeyDeleteAsync(CacheKeys.ListTest1);
// 从左侧插入
await service.ListLeftPushAsync(CacheKeys.ListTest1, "test0");
await service.ListLeftPushAsync(CacheKeys.ListTest1, "test1");
await service.ListLeftPushAsync(CacheKeys.ListTest1, "test2");
await service.ListLeftPushAsync(CacheKeys.ListTest1, "test3");
var leftLength = await service.ListLengthAsync(CacheKeys.ListTest1);
WriteMsg(CacheKeys.ListTest1, "左侧插入数量 " + leftLength);

await service.ListRightPushAsync(CacheKeys.ListTest1, "test4");
await service.ListRightPushAsync(CacheKeys.ListTest1, "test5");
await service.ListRightPushAsync(CacheKeys.ListTest1, "test6");
var rightLength = await service.ListLengthAsync(CacheKeys.ListTest1);
WriteMsg(CacheKeys.ListTest1, "左侧插入数量 " + (rightLength - leftLength));

var values = await service.ListRangeAsync(CacheKeys.ListTest1, 0, rightLength - 1);
foreach (var item in values)
{
    WriteMsg(CacheKeys.ListTest1, "list 值" + item.ToString());
}
//WriteMsg(CacheKeys.ListTest1, "Length " + keyLength);
Console.WriteLine("Hello, World!");



static void Write(string key, string message)
{
    Console.WriteLine("key:" + key + "    message:" + message);
}

static void WriteMsg(string key, string message)
{
    Console.WriteLine("key:" + key + "  " + message);
}

