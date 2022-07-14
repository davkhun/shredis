using Microsoft.Extensions.Caching.Memory;

namespace shredis.Infrastucture
{
    public class ShredisCache
    {
        public MemoryCache Cache { get; } = new MemoryCache(
            new MemoryCacheOptions
            {
                SizeLimit = Convert.ToInt32(Environment.GetEnvironmentVariable("CACHE_SIZE_LIMIT")==null? 1024: Environment.GetEnvironmentVariable("CACHE_SIZE_LIMIT")) 
            });
    }
}
