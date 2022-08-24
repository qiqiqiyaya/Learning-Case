

using DistributedLock.RedisHelper;
using DistributedLock.Server;

// 入队的code
int index = 100000;
while (index > 0)
{
    //string msg = Console.ReadLine();
    new MyRedisSubPublishHelper().PublishMessage("nihaofengge", $"你好风哥：Guid值是：{DateTime.Now}{Guid.NewGuid().ToString()}");
    Console.WriteLine("发布成功！");
    index -= 1;
}


// 秒杀的code

try
{
    Console.WriteLine("秒杀开始。。。。。");
    for (int i = 0; i < 200; i++)
    {
        Task.Run(() =>
        {
            MyRedisSubPublishHelper.LockByRedis("mstest");
            string productCount = MyRedisHelper.StringGet("productcount");
            int pcount = int.Parse(productCount);
            if (pcount > 0)
            {
                long dlong = MyRedisHelper.StringDec("productcount");
                Console.WriteLine($"秒杀成功，商品库存:{dlong}");
                pcount -= 1;
                System.Threading.Thread.Sleep(30);
            }
            else
            {
                Console.WriteLine($"秒杀失败，商品库存为零了！");
                throw new Exception("产品秒杀数量为零！");//加载这里会比较保险
            }
            MyRedisSubPublishHelper.UnLockByRedis("mstest");
        }).Wait();
    }
}
catch (Exception ex)
{
    Console.WriteLine($"产品已经秒杀完毕，原因：{ex.Message}");
}
Console.ReadKey();

Console.ReadKey();


