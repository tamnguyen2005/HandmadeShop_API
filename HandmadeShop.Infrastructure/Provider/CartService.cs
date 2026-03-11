using HandmadeShop.Application.Interfaces;
using HandmadeShop.Domain.Entities;

namespace HandmadeShop.Infrastructure.Provider
{
    public class CartService : ICartService
    {
        private readonly ICacheService _cache;

        public CartService(ICacheService cache)
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
            var data = await _cache.GetAsync<ShoppingCart>(key);
            if (data == null)
                return new ShoppingCart(userName);
            return data;
        }

        public async Task<ShoppingCart> UpdateCartAsync(ShoppingCart cart)
        {
            var key = $"cart:{cart.UserName}";
            await _cache.SetAsync(key, cart, TimeSpan.FromDays(30));
            return cart;
        }
    }
}