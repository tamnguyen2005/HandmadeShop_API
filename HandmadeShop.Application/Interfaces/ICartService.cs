using HandmadeShop.Domain.Entities;

namespace HandmadeShop.Application.Interfaces
{
    public interface ICartService
    {
        Task<ShoppingCart> GetCartAsync(string userName);

        Task<ShoppingCart> UpdateCartAsync(ShoppingCart cart);

        Task DeleteCartAsync(string userName);
    }
}