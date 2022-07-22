using Microsoft.Extensions.Caching.Memory;
using shredis.Models;
using System.Configuration;

namespace shredis.Infrastucture
{
    public class ShredisCache
    {
        private static IConfiguration _config;
        public ShredisCache(IConfiguration configuration)
        {
            _config = configuration;
        }

        public MemoryCache Cache { get; } = new MemoryCache(
            new MemoryCacheOptions
            {
                  SizeLimit = Convert.ToInt32(_config.GetSection("CacheSizeLimit").Value == null ? 
                      1024 : 
                     _config.GetSection("CacheSizeLimit"))
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
