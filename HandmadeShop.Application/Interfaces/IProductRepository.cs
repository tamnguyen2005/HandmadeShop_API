using HandmadeShop.Application.DTOs.Product;
using HandmadeShop.Domain.Entities;

namespace HandmadeShop.Application.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product?> GetProductWithDetailAsync(Guid id);

        Task<List<Product>> GetAllProductAsync(QueryProductRequest request);

        Task UpdateProductAsync(Guid id, string url, UpdateProductRequest request);
    }
}