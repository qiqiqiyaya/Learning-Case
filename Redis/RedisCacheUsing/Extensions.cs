using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace RedisCacheUsing
{
    public static class Extensions
    {
        public static HashEntry[] ToHashEntries(this Dictionary<string, string> source)
        {
            var hashEntries = new List<HashEntry>();
            foreach (var item in source)
            {
                hashEntries.Add(new HashEntry(item.Key, item.Value));
            }

            hashEntries.Add(new HashEntry("name", "item.Value"));
            return hashEntries.ToArray();
        }
    }
}
