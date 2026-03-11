namespace HandmadeShop.Application.Interfaces
{
    public interface ICacheService
    {
        public Task SetAsync<T>(string key, T value, TimeSpan? cacheTime = null);

        public Task RemoveAsync(string key);

        public Task<T?> GetAsync<T>(string key);
    }
}