using Microsoft.Extensions.Caching.Memory;
using shredis.Models;

namespace shredis.Infrastucture
{
    public class ShredisCache
    {
        public MemoryCache Cache { get; } = new MemoryCache(
            new MemoryCacheOptions
            {
                SizeLimit = Convert.ToInt32(Environment.GetEnvironmentVariable("CACHE_SIZE_LIMIT") == null ? 
                    1024 : 
                    Environment.GetEnvironmentVariable("CACHE_SIZE_LIMIT"))
            });

        public bool KeyExists(string key)
        {
            return Cache.TryGetValue(key, out var _);
        }

        public void SetValue(Request keyValue)
        {
            var obj = new ValueObject
            {
                Type = keyValue.Value.GetType().ToString(),
                Value = keyValue.Value
            };
            Cache.Set(keyValue.Key, obj, new MemoryCacheEntryOptions
            {
                Size = 1,
                AbsoluteExpirationRelativeToNow = keyValue.ExpirationTimeInMinutes.HasValue ? TimeSpan.FromMinutes(keyValue.ExpirationTimeInMinutes.Value) : null
            });
        }
    }
}
