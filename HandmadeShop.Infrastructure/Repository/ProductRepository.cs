using HandmadeShop.Application.DTOs.Product;
using HandmadeShop.Application.Interfaces;
using HandmadeShop.Application.Specifications;
using HandmadeShop.Domain.Entities;
using HandmadeShop.Infrastructure.Data;
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

        public async Task<List<Product>> GetAllProductAsync(QueryProductRequest request)
        {
            //var queryable = _context.Products.AsQueryable();
            //queryable = queryable.Where(p => p.IsDeleted == false);
            //if (!string.IsNullOrEmpty(request.Name))
            //    queryable = queryable.Where(p => p.Name.Contains(request.Name));
            //if (request.BasePrice.HasValue)
            //    queryable = queryable.Where(p => p.BasePrice > request.BasePrice);
            //var pageNumber = request.PageNumber ?? 1;
            //var pageSize = request.PageSize ?? 10;
            //var result = await queryable.OrderBy(p => p.Id)
            //                           .Skip((pageNumber - 1) * pageSize)
            //                           .Take(pageSize)
            //                           .Select(p => new ProductResponse()
            //                           {
            //                               Id = p.Id,
            //                               Name = p.Name,
            //                               Description = p.Description,
            //                               BasePrice = p.BasePrice,
            //                               StockQuantity = p.StockQuantity,
            //                               ImageURL = p.ImageURL,
            //                               CategoryName = p.Category.Name,
            //                               AverageReview = p.AverageRating,
            //                               ReviewCount = p.ReviewCount,
            //                           }).ToListAsync();
            var spec = new ProductSpecification(request);
            var query = SpecificationEvaluator<Product>.GetQuery(_context.Products.AsQueryable(), spec);
            var result = await query.ToListAsync();
            return result;
        }

        public async Task<Product?> GetProductWithDetailAsync(Guid id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.SubImages)
                //.Include(p => p.Reviews)
                .Include(p => p.Options)
                .ThenInclude(o => o.Values).FirstOrDefaultAsync(p => p.Id == id && p.IsDeleted == false);
        }

        public async Task UpdateProductAsync(Guid id, string url, UpdateProductRequest request)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null || product.IsDeleted == true)
                throw new KeyNotFoundException("Product does not exist !");
            if (!string.IsNullOrEmpty(request.Name))
                product.Name = request.Name;
            if (!string.IsNullOrEmpty(request.Description))
                product.Description = request.Description;
            if (request.BasePrice.HasValue)
                product.BasePrice = request.BasePrice.Value;
            if (request.StockQuantity.HasValue)
                product.StockQuantity = request.StockQuantity.Value;
            if (!string.IsNullOrEmpty(url))
            {
                product.ImageURL = url;
            }
            if (request.CategoryId.HasValue)
                product.CategoryId = request.CategoryId.Value;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}