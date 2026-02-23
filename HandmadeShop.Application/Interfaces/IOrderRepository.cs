using HandmadeShop.Application.DTOs.Order;
using HandmadeShop.Domain.Entities;

namespace HandmadeShop.Application.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<OrderDetailResponse> GetOrderByIdAsync(Guid id);
    }
}