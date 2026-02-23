using HandmadeShop.Application.Interfaces;
using HandmadeShop.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace HandmadeShop.Infrastructure.Provider
{
    public class CartService : ICartService
    {
        private readonly IDistributedCache _cache;

        public CartService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task DeleteCartAsync(string userName)
        {
            var key = $"cart:{userName}";
            await _cache.RemoveAsync(key);
        }

        public async Task<ShoppingCart> GetCartAsync(string userName)
        {
            var key = $"cart:{userName}";
            var data = await _cache.GetStringAsync(key);
            if (string.IsNullOrEmpty(data))
                return new ShoppingCart(userName);
            return JsonSerializer.Deserialize<ShoppingCart>(data);
        }

        public async Task<ShoppingCart> UpdateCartAsync(ShoppingCart cart)
        {
            var key = $"cart:{cart.UserName}";
            var data = JsonSerializer.Serialize(cart);
            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30)
            };
            await _cache.SetStringAsync(key, data, options);
            return cart;
        }
    }
}