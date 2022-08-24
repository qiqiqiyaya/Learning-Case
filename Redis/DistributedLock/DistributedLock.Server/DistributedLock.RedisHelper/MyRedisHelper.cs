using StackExchange.Redis;

namespace DistributedLock.RedisHelper
{
    public class MyRedisHelper
    {
        private static readonly string ConnectionRedisStr;
        static MyRedisHelper()
        {
            //在这里来初始化一些配置信息
            ConnectionRedisStr = "12.23.45.12:6379,connectTimeout=1000,connectRetry=3,syncTimeout=10000";
        }

        #region Redis string简单的常见同步方法操作
        public static bool StringSet(string key, string stringValue, double senconds = 60)
        {
            using (var conn = ConnectionMultiplexer.Connect(ConnectionRedisStr))
            {
                IDatabase db = conn.GetDatabase();
                return db.StringSet(key, stringValue, TimeSpan.FromSeconds(senconds));
            }
        }
        public static string StringGet(string key)
        {
            using (var conn = ConnectionMultiplexer.Connect(ConnectionRedisStr))
            {
                IDatabase db = conn.GetDatabase();
                return db.StringGet(key);
            }
        }

        public static long StringInc(string key)
        {
            using (var conn = ConnectionMultiplexer.Connect(ConnectionRedisStr))
            {
                IDatabase db = conn.GetDatabase();
                return db.StringIncrement(key);
            }
        }

        public static long StringDec(string key)
        {
            using (var conn = ConnectionMultiplexer.Connect(ConnectionRedisStr))
            {
                IDatabase db = conn.GetDatabase();
                return db.StringDecrement(key);
            }
        }
        public static bool KeyExists(string key)
        {
            using (var conn = ConnectionMultiplexer.Connect(ConnectionRedisStr))
            {
                IDatabase db = conn.GetDatabase();
                return db.KeyExists(key);
            }
        }
        #endregion

        #region List Hash, Set,Zset 大同小异的使用，比较简单,后续有时间再补上

        #endregion

        #region 入队出队

        #region 入队
        /// <summary>
        /// 入队right
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public static long EnqueueListRightPush(RedisKey queueName, RedisValue redisValue)
        {
            using (var conn = ConnectionMultiplexer.Connect(ConnectionRedisStr))
            {
                return conn.GetDatabase().ListRightPush(queueName, redisValue);
            }
        }
        /// <summary>
        /// 入队left
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="redisvalue"></param>
        /// <returns></returns>
        public static long EnqueueListLeftPush(RedisKey queueName, RedisValue redisvalue)
        {
            using (var conn = ConnectionMultiplexer.Connect(ConnectionRedisStr))
            {
                return conn.GetDatabase().ListLeftPush(queueName, redisvalue);
            }
        }
        /// <summary>
        /// 入队left异步
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="redisvalue"></param>
        /// <returns></returns>
        public static async Task<long> EnqueueListLeftPushAsync(RedisKey queueName, RedisValue redisvalue)
        {
            using (var conn = await ConnectionMultiplexer.ConnectAsync(ConnectionRedisStr))
            {
                return await conn.GetDatabase().ListLeftPushAsync(queueName, redisvalue);
            }
        }
        /// <summary>
        /// 获取队列的长度
        /// </summary>
        /// <param name="queueName"></param>
        /// <returns></returns>
        public static long EnqueueListLength(RedisKey queueName)
        {
            using (var conn = ConnectionMultiplexer.Connect(ConnectionRedisStr))
            {
                return conn.GetDatabase().ListLength(queueName);
            }
        }

        #endregion

        #region 出队
        public static string DequeueListPopLeft(RedisKey queueName)
        {
            using (var conn = ConnectionMultiplexer.Connect(ConnectionRedisStr))
            {
                IDatabase database = conn.GetDatabase();
                int count = database.ListRange(queueName).Length;
                if (count <= 0)
                {
                    throw new Exception($"队列{queueName}数据为零");
                }
                string redisValue = database.ListLeftPop(queueName);
                if (!string.IsNullOrEmpty(redisValue))
                    return redisValue;
                else
                    return string.Empty;
            }
        }
        public static string DequeueListPopRight(RedisKey queueName)
        {
            using (var conn = ConnectionMultiplexer.Connect(ConnectionRedisStr))
            {
                IDatabase database = conn.GetDatabase();
                int count = database.ListRange(queueName).Length;
                if (count <= 0)
                {
                    throw new Exception($"队列{queueName}数据为零");
                }
                string redisValue = conn.GetDatabase().ListRightPop(queueName);
                if (!string.IsNullOrEmpty(redisValue))
                    return redisValue;
                else
                    return string.Empty;
            }
        }
        public static async Task<string> DequeueListPopRightAsync(RedisKey queueName)
        {
            using (var conn = await ConnectionMultiplexer.ConnectAsync(ConnectionRedisStr))
            {
                IDatabase database = conn.GetDatabase();
                int count = (await database.ListRangeAsync(queueName)).Length;
                if (count <= 0)
                {
                    throw new Exception($"队列{queueName}数据为零");
                }
                string redisValue = await conn.GetDatabase().ListRightPopAsync(queueName);
                if (!string.IsNullOrEmpty(redisValue))
                    return redisValue;
                else
                    return string.Empty;
            }
        }
        #endregion

        #endregion

        #region 分布式锁
        public static void LockByRedis(string key, int expireTimeSeconds = 10)
        {
            try
            {
                IDatabase database1 = ConnectionMultiplexer.Connect(ConnectionRedisStr).GetDatabase();
                while (true)
                {
                    expireTimeSeconds = expireTimeSeconds > 20 ? 10 : expireTimeSeconds;
                    bool lockflag = database1.LockTake(key, Thread.CurrentThread.ManagedThreadId, TimeSpan.FromSeconds(expireTimeSeconds));
                    if (lockflag)
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Redis加锁异常:原因{ex.Message}");
            }
        }

        public static bool UnLockByRedis(string key)
        {
            ConnectionMultiplexer conn = ConnectionMultiplexer.Connect(ConnectionRedisStr);
            try
            {
                IDatabase database1 = conn.GetDatabase();
                return database1.LockRelease(key, Thread.CurrentThread.ManagedThreadId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Redis加锁异常:原因{ex.Message}");
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }
        #endregion

    }
}