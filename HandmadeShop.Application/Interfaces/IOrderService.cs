using HandmadeShop.Application.DTOs.Order;

namespace HandmadeShop.Application.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrderAsync(CreateOrderRequest request);
    }
}