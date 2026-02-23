using HandmadeShop.Application.DTOs.Order;
using HandmadeShop.Application.Interfaces;
using HandmadeShop.Domain.Entities;
using HandmadeShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HandmadeShop.Infrastructure.Repository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly HandmadeShopDBContext _context;

        public OrderRepository(HandmadeShopDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<OrderDetailResponse> GetOrderByIdAsync(Guid id)
        {
            var order = await _context.Orders.Include(o => o.Items).Select(o => new OrderDetailResponse()
            {
                Id = id,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                ShippingAddress = o.ShippingAddress,
                PaymentMethod = o.PaymentMethod,
                Status = o.Status,
                Products = o.Items.Select(i => new MiniProductResponse()
                {
                    Id = i.Id,
                    Name = i.ProductName,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    Configurations = i.Configuration,
                }).ToList()
            }).FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
                throw new KeyNotFoundException("Order does not exist !");
            return order;
        }
    }
}