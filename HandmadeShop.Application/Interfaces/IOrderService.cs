using HandmadeShop.Application.DTOs.Order;

namespace HandmadeShop.Application.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrderAsync(CreateOrderRequest request);

        Task UpdateOrderStatusAsync(Guid id, string action);

        Task<List<OrderResponse>> GetAllUserOrderAsync();

        Task<OrderDetailResponse> GetOrderDetailAsync(Guid id);

        Task DeleteOrder(Guid id);
    }
}