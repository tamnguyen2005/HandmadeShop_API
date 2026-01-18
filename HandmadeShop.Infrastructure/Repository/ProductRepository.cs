using HandmadeShop.Application.Interfaces;
using HandmadeShop.Domain.Entities;
using HandmadeShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HandmadeShop.Infrastructure.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly HandmadeShopDBContext _context;

        public ProductRepository(HandmadeShopDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Product?> GetProductWithDetailAsync(Guid id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Options)
                .ThenInclude(o => o.Values).FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}