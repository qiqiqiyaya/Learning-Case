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
        protected readonly TimeSpan ExpiryTime = TimeSpan.FromMinutes(30);

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
        public Task<bool> StringSetAsync(string key, RedisValue value)
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

        public async Task<bool> StringGetBitAsync(string key, long offset)
        {
            var value = await RedisCache.StringGetBitAsync(key, offset);
            return value;
        }

        public async Task<bool> StringSetBitAsync(string key, long offset,bool bit)
        {
            var value = await RedisCache.StringSetBitAsync(key, offset, bit);
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

        public async Task<RedisValue[]> SetMembersAsync(string key)
        {
            var result = await RedisCache.SetMembersAsync(key);
            return result;
        }

        /// <summary>
        /// 并集
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="key2"></param>
        /// <returns></returns>
        public async Task<RedisValue[]> SetUnionAsync(string key1,string key2)
        {
            var result = await RedisCache.SetCombineAsync(SetOperation.Union, key1, key2);
            return result;
        }

        /// <summary>
        /// 差集
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="key2"></param>
        /// <returns></returns>
        public async Task<RedisValue[]> SetDifferenceAsync(string key1, string key2)
        {
            var result = await RedisCache.SetCombineAsync(SetOperation.Difference, key1, key2);
            return result;
        }

        /// <summary>
        /// 交集
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="key2"></param>
        /// <returns></returns>
        public async Task<RedisValue[]> SetIntersectAsync(string key1, string key2)
        {
            var result = await RedisCache.SetCombineAsync(SetOperation.Intersect, key1, key2);
            return result;
        }

        public async Task<long> SetCombineAndStoreAsync(string newKey, string key1, string key2)
        {
            var result = await RedisCache.SetCombineAndStoreAsync(SetOperation.Union,newKey, key1, key2);
            return result;
        }

        //SortedSet  
        public Task<long> SortedSetAddAsync(string key, params SortedSetEntry[] sortedSetEntries)
        {
            return RedisCache.SortedSetAddAsync(key, sortedSetEntries);
        }

        public Task<RedisValue[]> SortedSetRangeByRankAsync(string key, long start, long stop)
        {
            return RedisCache.SortedSetRangeByRankAsync(key, start, stop);
        }

        public Task<long> SortedSetCombineAndStoreAsync(SetOperation operation, RedisKey newKey, RedisKey key1, RedisKey key2)
        {
            return RedisCache.SortedSetCombineAndStoreAsync(operation, newKey, key1, key2);
        }

        public Task<long?> SortedSetRankAsync(RedisKey key,RedisValue member,Order order)
        {
            return RedisCache.SortedSetRankAsync(key, member, order);
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
            var aa = await RedisCache.HashGetLeaseAsync(key, field);
            return aa.DecodeString();
        }

        public IAsyncEnumerable<HashEntry> HashScanAsync(string key)
        {
            return RedisCache.HashScanAsync(key);
        }

        public IAsyncEnumerable<HashEntry> HashScanAsync(string key,string pattern, int pageSize, int cursor, int pageOffset)
        {
            return RedisCache.HashScanAsync(key, pattern, pageSize, cursor, pageOffset);
        }


        // list
        /// <summary>
        /// 如果list不存在，则创建，从左侧插入值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<long> ListLeftPushAsync(string key, RedisValue value)
        {
            return RedisCache.ListLeftPushAsync(key, value);
        }

        /// <summary>
        /// 如果list不存在，则创建，从右侧插入值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<long> ListRightPushAsync(string key, RedisValue value)
        {
            var aa = RedisCache.ListGetByIndex("", 1);
            return RedisCache.ListRightPushAsync(key, value);
        }
        
        public Task<RedisValue[]> ListRangeAsync(string key, long begin, long stop)
        {
            return RedisCache.ListRangeAsync(key, begin, stop);
        }

        public Task<long> ListLengthAsync(string key)
        {
            return RedisCache.ListLengthAsync(key);
        }

        public Task<RedisValue> ListLengthAsync(string key,long index)
        {
            return RedisCache.ListGetByIndexAsync(key, index);
        }

        /// <summary>
        /// 删除对应缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<bool> KeyDeleteAsync(string key)
        {
            return RedisCache.KeyDeleteAsync(key);
        }
    }
}
