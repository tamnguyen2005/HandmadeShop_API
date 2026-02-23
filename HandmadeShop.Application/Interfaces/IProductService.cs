using HandmadeShop.Application.DTOs.Product;

namespace HandmadeShop.Application.Interfaces
{
    public interface IProductService
    {
        Task CreateProductAsync(CreateProductRequest request);

        Task<ProductResponse?> GetProductByIdAsync(Guid id);

        Task<List<ProductResponse>> GetAllProductAsync(QueryProductRequest request);

        Task UpdateProductAsync(Guid id, UpdateProductRequest request);

        Task DeleteProduct(Guid id);
    }
}