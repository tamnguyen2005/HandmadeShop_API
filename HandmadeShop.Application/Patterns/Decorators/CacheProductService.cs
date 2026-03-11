using HandmadeShop.Application.DTOs.Product;
using HandmadeShop.Application.Interfaces;

namespace HandmadeShop.Application.Patterns.Decorators
{
    public class CacheProductService : IProductService
    {
        private readonly IProductService _innerService;
        private readonly ICacheService _cache;

        public CacheProductService(IProductService innerService, ICacheService cache)
        {
            _innerService = innerService;
            _cache = cache;
        }

        public async Task CreateProductAsync(CreateProductRequest request)
        {
            await _innerService.CreateProductAsync(request);
        }

        public async Task DeleteProduct(Guid id)
        {
            await _innerService.DeleteProduct(id);
        }

        public async Task<List<ProductResponse>> GetAllProductAsync(QueryProductRequest request)
        {
            string key = $"p_page{request.PageNumber}";
            var data = await _cache.GetAsync<List<ProductResponse>>(key);
            if (data == null)
            {
                Console.WriteLine("CACHE-MISS");
                data = await _innerService.GetAllProductAsync(request);
                await _cache.SetAsync(key, data, TimeSpan.FromHours(1));
            }
            else
                Console.WriteLine("CACHE-HIT");
            return data;
        }

        public async Task<ProductResponse?> GetProductByIdAsync(Guid id)
        {
            string key = $"p_{id}";
            var data = await _cache.GetAsync<ProductResponse>(key);
            if (data == null)
            {
                Console.WriteLine("CACHE-MISS");
                data = await _innerService.GetProductByIdAsync(id);
                await _cache.SetAsync(key, data, TimeSpan.FromHours(1));
            }
            else
                Console.WriteLine("CACHE-HIT");
            return data;
        }

        public async Task UpdateProductAsync(Guid id, UpdateProductRequest request)
        {
            await _innerService.UpdateProductAsync(id, request);
        }
    }
}