using HandmadeShop.Application.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace HandmadeShop.Infrastructure.Cache
{
    public class RedisService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;

        public RedisService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var cachedData = await _distributedCache.GetStringAsync(key);
            if (string.IsNullOrEmpty(cachedData))
            {
                return default;
            }
            return JsonSerializer.Deserialize<T>(cachedData);
        }

        public async Task RemoveAsync(string key)
        {
            await _distributedCache.RemoveAsync(key);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? cacheTime = null)
        {
            var serialized = JsonSerializer.Serialize(value);
            var options = new DistributedCacheEntryOptions();
            if (cacheTime.HasValue)
            {
                options.AbsoluteExpirationRelativeToNow = cacheTime;
            }
            await _distributedCache.SetStringAsync(key, serialized, options);
        }
    }
}