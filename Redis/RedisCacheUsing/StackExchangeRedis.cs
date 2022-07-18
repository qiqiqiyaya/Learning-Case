using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace RedisCacheUsing
{
    public class StackExchangeRedis
    {
        protected readonly ConnectionMultiplexer ConnectionMultiplexer;
        protected readonly IDatabase RedisCache;
        protected readonly TimeSpan ExpiryTime = TimeSpan.FromMinutes(1);

        public StackExchangeRedis(ConnectionMultiplexer connectionMultiplexer)
        {
            ConnectionMultiplexer = connectionMultiplexer;
            RedisCache = ConnectionMultiplexer.GetDatabase(0);
        }

        //string
        /// <summary>
        /// 添加一个string类型的元素。该string类型的Key将被设置默认过期时间
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public Task<bool> StringSetAsync(string key, string value)
        {
            return RedisCache.StringSetAsync(key, value, ExpiryTime);
        }

        public Task<long> StringAppendAsync(string key, string value)
        {
            return RedisCache.StringAppendAsync(key, value);
        }

        public async Task<string> StringGetAsync(string key)
        {
            var value = await RedisCache.StringGetAsync(key);
            if (value.IsNullOrEmpty)
            {
                throw new NullReferenceException();
            }

            return value.ToString();
        }

        public async Task<bool> StringGetBitAsync(string key)
        {
            var value = await RedisCache.StringGetBitAsync(key,0);
            return value;
        }

        public async Task<long> StringBitCountAsync(string key)
        {
            var value = await RedisCache.StringBitCountAsync(key);
            return value;
        }

        public async Task<string> StringGetRangeAsync(string key,long start,long end)
        {
            var value = await RedisCache.StringGetRangeAsync(key,start,end);
            return value.ToString();
        }

        // Set 
        /// <summary>
        /// 创建Set集合，并向其中添加元素。该Set的Key将被设置默认过期时间
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="values">值集合</param>
        /// <returns></returns>
        public async Task<long> SetCreateAsync(string key, params string[] values)
        {
            var redisValues = values.Select(s => new RedisValue(s)).ToArray();

            var result = await RedisCache.SetAddAsync(key, redisValues);
            await RedisCache.KeyExpireAsync(key, ExpiryTime);
            return result;
        }

        //SortedSet  
        public Task<long> SortedSetAddAsync(string key, params SortedSetEntry[] sortedSetEntries)
        {
            return RedisCache.SortedSetAddAsync(key, sortedSetEntries);
        }

        // hash
        public Task HashSetAsync(string key, params HashEntry[] entries)
        {
            return RedisCache.HashSetAsync(key, entries);
        }

        public Task<HashEntry[]> HashGetAllAsync(string key)
        {
            return RedisCache.HashGetAllAsync(key);
        }

        public Task<double> HashDecrementAsync(string key,string field,double value)
        {
            return RedisCache.HashDecrementAsync(key,field, value);
        }

        public async Task<string?> HashGetLeaseAsync(string key, string field)
        {
            var aa=await RedisCache.HashGetLeaseAsync(key, field);
            return aa.DecodeString();
        }
        
        // list
        public Task ListSetByIndexAsync(string key, long index, string value)
        {
            return RedisCache.ListSetByIndexAsync(key, index, value);
        }
    }
}
