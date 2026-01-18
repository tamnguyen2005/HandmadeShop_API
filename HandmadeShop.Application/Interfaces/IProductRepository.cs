using HandmadeShop.Domain.Entities;

namespace HandmadeShop.Application.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product?> GetProductWithDetailAsync(Guid id);
    }
}