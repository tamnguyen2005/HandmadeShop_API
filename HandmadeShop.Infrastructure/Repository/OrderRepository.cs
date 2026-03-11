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

        public async Task<Order?> GetOrderByIdAsync(Guid id)
        {
            var order = await _context.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id);
            return order;
        }
    }
}