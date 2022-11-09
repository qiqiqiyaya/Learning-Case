
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

await service.StringSetBitAsync(CacheKeys.StringTest2, 7, Convert.ToBoolean(0));
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
profile.Add("age", "22");
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
    WriteMsg(CacheKeys.HashTest1, entry.Name + ": " + entry.Value);
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
var pagelist = service.HashScanAsync(CacheKeys.HashTest1, "f*", 2, 0, 0);
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

await service.ListRightPushAsync(CacheKeys.ListTest1, 1);
await service.ListRightPushAsync(CacheKeys.ListTest1, "test5");
await service.ListRightPushAsync(CacheKeys.ListTest1, "test6");
await service.ListRightPushAsync(CacheKeys.ListTest1, "test6");
var rightLength = await service.ListLengthAsync(CacheKeys.ListTest1);
WriteMsg(CacheKeys.ListTest1, "左侧插入数量 " + (rightLength - leftLength));

var values = await service.ListRangeAsync(CacheKeys.ListTest1, 0, rightLength - 1);
foreach (var item in values)
{
    WriteMsg(CacheKeys.ListTest1, "list 值" + item.ToString());
}

Console.WriteLine(" ");
Console.WriteLine(" ");
Console.WriteLine("Set");
await service.SetCreateAsync(CacheKeys.SetTest1, "1", "2", "3", "3", "3", "3", "4", "5", "6", "11");
await service.SetCreateAsync(CacheKeys.SetTest2, "8", "7", "40", "10", "11", "11", "12", "12", "13");
var array = await service.SetMembersAsync(CacheKeys.SetTest1);
foreach (var value in array)
{
    WriteMsg(CacheKeys.SetTest1, "set 值" + value.ToString());
}

var union = await service.SetUnionAsync(CacheKeys.SetTest1, CacheKeys.SetTest2);
foreach (var value in union)
{
    WriteMsg("Set 并集", "set 值" + value.ToString());
}

var difference = await service.SetDifferenceAsync(CacheKeys.SetTest1, CacheKeys.SetTest2);
foreach (var value in difference)
{
    WriteMsg("Set 差集", "set 值" + value.ToString());
}

var intersect = await service.SetIntersectAsync(CacheKeys.SetTest1, CacheKeys.SetTest2);
foreach (var value in intersect)
{
    WriteMsg("Set 交集", "set 值" + value.ToString());
}

Console.WriteLine("");
await service.SetCombineAndStoreAsync(CacheKeys.SetTest3, CacheKeys.SetTest1, CacheKeys.SetTest2);
var array3 = await service.SetMembersAsync(CacheKeys.SetTest3);
foreach (var value in array3)
{
    WriteMsg(CacheKeys.SetTest1 + "与" + CacheKeys.SetTest2 + "的交集生产新Set " + CacheKeys.SetTest3, "set 值" + value.ToString());
}

Console.WriteLine("");
Console.WriteLine("");
Console.WriteLine("");
Console.WriteLine("Sort Set");
var array4 = await service.SortedSetAddAsync(CacheKeys.ZSetTest1,
    new SortedSetEntry("1", 1),
    new SortedSetEntry("2", 1),
    new SortedSetEntry("3", 2),
    new SortedSetEntry("3", 3),
    new SortedSetEntry("4", 4),
    new SortedSetEntry("5", 5),
    new SortedSetEntry("6", 6),
    new SortedSetEntry("7", 7),
    new SortedSetEntry("8", 8),
    new SortedSetEntry("9", 9));
await service.SortedSetAddAsync(CacheKeys.ZSetTest2,
    new SortedSetEntry("1", 1),
    new SortedSetEntry("12", 1),
    new SortedSetEntry("3", 2),
    new SortedSetEntry("13", 3),
    new SortedSetEntry("14", 4),
    new SortedSetEntry("5", 5),
    new SortedSetEntry("6", 6),
    new SortedSetEntry("17", 7),
    new SortedSetEntry("18", 8),
    new SortedSetEntry("9", 9));

var array5 = await service.SortedSetRangeByRankAsync(CacheKeys.ZSetTest1, 0, 8);
foreach (var value in array5)
{
    WriteMsg("ZSet", "ZSet 值" + value.ToString());
}

Console.WriteLine("");
Console.WriteLine("");
var setLength= await service.SortedSetCombineAndStoreAsync(SetOperation.Union, CacheKeys.ZSetTest3, CacheKeys.ZSetTest1,
    CacheKeys.ZSetTest2);
var array6 = await service.SortedSetRangeByRankAsync(CacheKeys.ZSetTest3, 0, setLength);
foreach (var value in array6)
{
    WriteMsg("ZSet", "ZSet 值" + value.ToString());
}

var longValue= await service.SortedSetRankAsync(CacheKeys.ZSetTest1, "1", Order.Ascending);
if (longValue.HasValue)
{
    WriteMsg("longValue", "longValue  " + longValue.Value);
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

