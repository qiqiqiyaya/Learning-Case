
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

Console.WriteLine("Hello, World!");
