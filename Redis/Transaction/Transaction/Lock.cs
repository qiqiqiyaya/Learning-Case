using StackExchange.Redis;
using System.Runtime.CompilerServices;

namespace Transaction
{
    public static class Lock
    {
        /// <summary>
        /// 使用Redis分布式锁执行某些操作
        /// </summary>
        /// <param name="lockName">锁名</param>
        /// <param name="act">操作</param>
        /// <param name="expiry">锁过期时间，若超出时间自动解锁 单位：sec</param>
        /// <param name="retry">获取锁的重复次数</param>
        /// <param name="tryDelay">获取锁的重试间隔  单位：ms</param>
        public static void LockAction(this IDatabase db, string lockName, Action act, int expiry = 10, int retry = 3, int tryDelay = 200)
        {
            if (act.Method.IsDefined(typeof(AsyncStateMachineAttribute), false))
            {
                throw new ArgumentException("使用异步Action请调用LockActionAsync");
            }

            TimeSpan exp = TimeSpan.FromSeconds(expiry);
            string token = Guid.NewGuid().ToString("N");
            try
            {
                bool ok = false;
                // 延迟重试
                for (int test = 0; test < retry; test++)
                {
                    if (db.LockTake(lockName, token, exp))
                    {
                        ok = true;
                        break;
                    }
                    else
                    {
                        Task.Delay(tryDelay).Wait();
                    }
                }
                if (!ok)
                {
                    throw new InvalidOperationException($"获取锁[{lockName}]失败");
                }
                act();
            }
            finally
            {
                db.LockRelease(lockName, token);
            }
        }

        /// <summary>
        /// 使用Redis分布式锁执行某些异步操作
        /// </summary>
        /// <param name="lockName">锁名</param>
        /// <param name="act">操作</param>
        /// <param name="expiry">锁过期时间，若超出时间自动解锁 单位：sec</param>
        /// <param name="retry">获取锁的重复次数</param>
        /// <param name="tryDelay">获取锁的重试间隔  单位：ms</param>
        public static async Task LockActionAsync(this IDatabase db, string lockName, Func<Task> act, int expiry = 10, int retry = 10, int tryDelay = 1000)
        {
            TimeSpan exp = TimeSpan.FromSeconds(expiry);
            string token = Guid.NewGuid().ToString("N");
            try
            {
                bool ok = false;
                // 延迟重试
                for (int test = 0; test < retry; test++)
                {
                    Console.WriteLine("等待加锁...");
                    if (await db.LockTakeAsync(lockName, token, exp))
                    {
                        Console.WriteLine("--------------------加锁完成...");
                        ok = true;
                        break;
                    }
                    else
                    {
                        await Task.Delay(tryDelay);
                    }
                }
                if (!ok)
                {
                    throw new InvalidOperationException($"获取锁[{lockName}]失败");
                }
                await act();
            }
            finally
            {
                Console.WriteLine("-----锁释放...");
                await db.LockReleaseAsync(lockName, token);
            }
        }

    }
}
