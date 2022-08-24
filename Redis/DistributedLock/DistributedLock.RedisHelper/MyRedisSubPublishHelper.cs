using StackExchange.Redis;

namespace DistributedLock.Server
{
    public class MyRedisSubPublishHelper
    {
        private static readonly string redisConnectionStr = "127.0.0.1:6379,password=123456,abortConnect=false";
        private static readonly ConnectionMultiplexer _connectionMultiplexer;
        private static readonly IDatabase _database;

        static MyRedisSubPublishHelper()
        {
            _connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionStr);
            _database = _connectionMultiplexer.GetDatabase();
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
            return await _database.ListLeftPushAsync(queueName, redisvalue);
        }

        /// <summary>
        /// 出对
        /// </summary>
        /// <param name="queueName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task<string> DequeueListPopRightAsync(RedisKey queueName)
        {
            long count = await _database.ListLengthAsync(queueName);
            if (count <= 0)
            {
                throw new Exception($"队列{queueName}数据为零");
            }
            string redisValue = await _database.ListRightPopAsync(queueName);
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
                while (true)
                {
                    expireTimeSeconds = expireTimeSeconds > 20 ? 10 : expireTimeSeconds;
                    bool lockflag = _database.LockTake(key, Thread.CurrentThread.ManagedThreadId, TimeSpan.FromSeconds(expireTimeSeconds));
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