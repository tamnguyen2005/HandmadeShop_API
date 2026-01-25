using HandmadeShop.Application.DTOs.Product;
using HandmadeShop.Application.Interfaces;
using HandmadeShop.Application.Patterns.Builders;
using Microsoft.EntityFrameworkCore;

namespace HandmadeShop.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateProductAsync(CreateProductRequest request)
        {
            var category = _unitOfWork.Categories.GetByIdAsync(request.CategoryId);
            if (category == null)
            {
                throw new KeyNotFoundException("Category does not exist !");
            }
            var builder = new ProductBuilder().WithBaseInfo(request.Name, request.Description, request.BasePrice, request.StockQuantity, request.CategoryId);
            foreach (var o in request.Options)
            {
                builder.AddOption(o.Name, o.Values);
            }
            var product = builder.Build();
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<ProductResponse>> GetAllProductAsync(QueryProductRequest request)
        {
            var queryable = _unitOfWork.Products.AsQueryable();
            if (!string.IsNullOrEmpty(request.Name))
                queryable = queryable.Where(p => p.Name.Contains(request.Name));
            if (request.BasePrice.HasValue)
                queryable = queryable.Where(p => p.BasePrice > request.BasePrice);
            var pageNumber = request.PageNumber ?? 1;
            var pageSize = request.PageSize ?? 10;
            var result = await queryable.OrderBy(p => p.Id)
                                       .Skip((pageNumber - 1) * pageSize)
                                       .Take(pageSize)
                                       .Select(p => new ProductResponse()
                                       {
                                           Id = p.Id,
                                           Name = p.Name,
                                           Description = p.Description,
                                           BasePrice = p.BasePrice,
                                           StockQuantity = p.StockQuantity,
                                           ImageURL = p.ImageURL,
                                           CategoryName = p.Category.Name,
                                           Options = p.Options.Select(o => new ProductOptionResponse()
                                           {
                                               Name = o.Name,
                                               Values = o.Values.Select(v => v.Value).ToList()
                                           }).ToList()
                                       }).ToListAsync();
            return result;
        }

        public async Task<ProductResponse?> GetProductByIdAsync(Guid id)
        {
            var product = await _unitOfWork.Products.GetProductWithDetailAsync(id);
            if (product == null)
                throw new KeyNotFoundException("Product does not exist !");
            return new ProductResponse()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                BasePrice = product.BasePrice,
                StockQuantity = product.StockQuantity,
                ImageURL = product.ImageURL,
                CategoryName = product.Category.Name,
                Options = product.Options
                .Select(o => new ProductOptionResponse()
                {
                    Name = o.Name,
                    Values = o.Values.Select(v => v.Value).ToList()
                }).ToList()
            };
        }
    }
}