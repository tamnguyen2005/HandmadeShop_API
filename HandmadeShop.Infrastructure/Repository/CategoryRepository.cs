using HandmadeShop.Application.Interfaces;
using HandmadeShop.Domain.Entities;
using HandmadeShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HandmadeShop.Infrastructure.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly HandmadeShopDBContext _context;

        public CategoryRepository(HandmadeShopDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllCategoryAsync()
        {
            var categories = await _context.Categories.Where(c => c.ParentId == null).ToListAsync();
            return categories;
        }

        public async Task<List<Category>> GetALlCollectionAsync()
        {
            var categories = await _context.Categories.Where(c => c.IsCollection == true && c.IsDeleted == false).ToListAsync();
            return categories;
        }

        public async Task<Category?> GetCategoryByIdAsync(Guid id)
        {
            var category = await _context.Categories.Include(c => c.SubCategories)
                                                    .FirstOrDefaultAsync();
            return category;
        }
    }
}