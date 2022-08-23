// See https://aka.ms/new-console-template for more information

using StackExchange.Redis;
using Transaction;

ConfigurationOptions configuration = ConfigurationOptions.Parse("127.0.0.1:6379,password=123456,abortConnect=false");
ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(configuration);
var redisCache = connectionMultiplexer.GetDatabase(0);

//var multi = redisCache.Multiplexer;

//var tran = redisCache.CreateTransaction();

//tran.AddCondition(Condition.KeyExists("test"));
//var testValue = tran.StringSetAsync("test", "sfsafdsafdsa");
//var result = await tran.ExecuteAsync();
//if (result)
//{
//    Console.WriteLine("true: " + testValue.Status);
//}
//else
//{
//    Console.WriteLine("false: " + testValue.Status);
//}


//string name = redisCache.StringGet("name");
//string age = redisCache.StringGet("age");
//var tran1 = redisCache.CreateTransaction();//创建事物
//tran1.AddCondition(Condition.StringEqual("name", name));//乐观锁
//tran1.StringSetAsync("name", "海");
//tran1.StringSetAsync("age", 25);
//redisCache.StringSet("name", "Cang");//此时更改 name 值，提交事物的时候会失败。
//bool committed = tran1.Execute();//提交事物，true成功，false回滚。
//if (committed)
//{
//    Console.WriteLine("true: " + testValue.Status);
//}
//else
//{
//    Console.WriteLine("false: " + testValue.Status);
//}


//var batch = redisCache.CreateBatch();

////批量写
//Task t1 = batch.StringSetAsync("name", "羽");
//Task t2 = batch.StringSetAsync("age", 22);
//batch.Execute();
//Task.WaitAll(t1, t2);
//Console.WriteLine("Age:" + redisCache.StringGet("age"));
//Console.WriteLine("Name:" + redisCache.StringGet("name"));

////批量写
//for (int i = 0; i < 100000; i++)
//{
//    batch.StringSetAsync("age" + i, i);
//}
//batch.Execute();

////批量读
//List<Task<RedisValue>> valueList = new List<Task<RedisValue>>();
//for (int i = 0; i < 10000; i++)
//{
//    Task<RedisValue> tres = batch.StringGetAsync("age" + i);
//    valueList.Add(tres);
//}
//batch.Execute();
//foreach (var redisValue in valueList)
//{
//    string value = redisValue.Result;//取出对应的value值
//    Console.WriteLine(value);
//}


//// 由于 Redis 是单线程模型，命令操作原子性，所以利用这个特性可以很容易的实现分布式锁。
//RedisValue token = Environment.MachineName;
//// lock_key表示的是redis数据库中该锁的名称，不可重复。 
//// token用来标识谁拥有该锁并用来释放锁。
//// TimeSpan表示该锁的有效时间。10秒后自动释放，避免死锁。
//Console.WriteLine("等待加锁...");
//if (redisCache.LockTake("lock_key", token, TimeSpan.FromSeconds(10)))
//{
//    Console.WriteLine("加锁完成...");
//    try
//    {
//        //TODO:开始做你需要的事情
//        Thread.Sleep(10000);
//    }
//    finally
//    {
//        Console.WriteLine("锁释放...");
//        redisCache.LockRelease("lock_key", token);//释放锁
//    }
//}

// 带异步操作的用 LockActionAsync
await redisCache.LockActionAsync("MyLockName", async () =>
    {
        // 执行异步方法...
        await Task.Delay(5000);
        Console.WriteLine("Done");
    });

Console.ReadKey();


