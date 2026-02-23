using HandmadeShop.Application.DTOs.Category;
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

        public async Task<DetailCategoryResponse> GetCategoryByIdAsync(Guid id)
        {
            var category = await _context.Categories.Include(c => c.Products)
                                                  .Include(c => c.SubCategories)
                                                  .FirstOrDefaultAsync();
            if (category == null || category.IsDeleted)
                throw new KeyNotFoundException("Category does not exist !");
            var products = new List<MiniProductResponse>();
            if (category.Products != null && category.Products.Count != 0)
            {
                foreach (var product in category.Products)
                {
                    products.Add(new MiniProductResponse()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        BasePrice = product.BasePrice,
                        ImageURL = product.ImageURL,
                    });
                }
            }
            var result = new DetailCategoryResponse()
            {
                Id = category.Id,
                Name = category.Name,
                SubCategory = category.SubCategories == null
                                    ? new List<CategoryResponse>()
                                    : category.SubCategories.Where(s => s.IsDeleted == false).Select(s => new CategoryResponse()
                                    {
                                        Id = s.Id,
                                        Name = s.Name
                                    })
                                    .ToList(),
                Products = products
            };
            return result;
        }
    }
}