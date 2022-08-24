using StackExchange.Redis;

namespace DistributedLock.Server
{
    public class MyRedisSubPublishHelper
    {
        private static readonly string redisConnectionStr = "127.0.0.1:6379,connectTimeout=10000,connectRetry=3,syncTimeout=10000";
        private static readonly ConnectionMultiplexer _connectionMultiplexer;

        static MyRedisSubPublishHelper()
        {
            _connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionStr);
        }
        
        #region 发布订阅
        public void Subscriber(string ticName, Action<RedisChannel, RedisValue>? handler = null)
        {
            ISubscriber subscriber = _connectionMultiplexer.GetSubscriber();
            ChannelMessageQueue channelMessageQueue = subscriber.Subscribe(ticName);
            channelMessageQueue.OnMessage(channelMessage =>
            {
                if (handler != null)
                {
                    string redisChannel = channelMessage.Channel;
                    string msg = channelMessage.Message;
                    handler.Invoke(redisChannel, msg);
                }
                else
                {
                    string msg = channelMessage.Message;
                    Console.WriteLine($"订阅到消息: { msg},Channel={channelMessage.Channel}");
                }
            });
        }
        public void PublishMessage(string topticName, string message)
        {
            ISubscriber subscriber = _connectionMultiplexer.GetSubscriber();
            long publishLong = subscriber.Publish(topticName, message);
            Console.WriteLine($"发布消息成功：{publishLong}");
        }
        #endregion

        #region 入队出队
        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="redisvalue"></param>
        /// <returns></returns>
        public static async Task<long> EnqueueListLeftPushAsync(RedisKey queueName, RedisValue redisvalue)
        {
            return await _connectionMultiplexer.GetDatabase().ListLeftPushAsync(queueName, redisvalue);
        }

        /// <summary>
        /// 出对
        /// </summary>
        /// <param name="queueName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task<string> DequeueListPopRightAsync(RedisKey queueName)
        {
            IDatabase database = _connectionMultiplexer.GetDatabase();
            int count = (await database.ListRangeAsync(queueName)).Length;
            if (count <= 0)
            {
                throw new Exception($"队列{queueName}数据为零");
            }
            string redisValue = await database.ListRightPopAsync(queueName);
            if (!string.IsNullOrEmpty(redisValue))
                return redisValue;
            else
                return string.Empty;
        }
        #endregion

        #region 分布式锁
        public static void LockByRedis(string key, int expireTimeSeconds = 10)
        {
            try
            {
                IDatabase database = _connectionMultiplexer.GetDatabase();
                while (true)
                {
                    expireTimeSeconds = expireTimeSeconds > 20 ? 10 : expireTimeSeconds;
                    bool lockflag = database.LockTake(key, Thread.CurrentThread.ManagedThreadId, TimeSpan.FromSeconds(expireTimeSeconds));
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
            try
            {
                IDatabase database = _connectionMultiplexer.GetDatabase();
                return database.LockRelease(key, Thread.CurrentThread.ManagedThreadId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Redis加锁异常:原因{ex.Message}");
            }
        }
        #endregion
    }
}