using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RedisExample.Services
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDistributedCache cache;
        public RedisCacheService(IDistributedCache _cache)
        {
            cache = _cache;
        }
        public T Get<T>(string key)
        {
            var value = cache.GetString(key);
            if (value != null)
            {
                return JsonSerializer.Deserialize<T>(value);
            }

            return default;
        }

        public T Set<T>(string key, T value)
        {
            var timeOut = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24),
                SlidingExpiration = TimeSpan.FromMinutes(60)
            };

            cache.SetString(key, JsonSerializer.Serialize(value), timeOut);

            return value;
        }
    }
}
