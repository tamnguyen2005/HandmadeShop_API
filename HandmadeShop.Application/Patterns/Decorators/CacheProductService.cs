using HandmadeShop.Application.DTOs.Product;
using HandmadeShop.Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace HandmadeShop.Application.Patterns.Decorators
{
    public class CacheProductService : IProductService
    {
        private readonly IProductService _innerService;
        private readonly IMemoryCache _cache;

        public CacheProductService(IProductService innerService, IMemoryCache cache)
        {
            _innerService = innerService;
            _cache = cache;
        }

        public async Task CreateProductAsync(CreateProductRequest request)
        {
            await _innerService.CreateProductAsync(request);
        }

        public async Task<List<ProductResponse>> GetAllProductAsync(QueryProductRequest request)
        {
            string key = $"p_page{request.PageNumber}";
            if (_cache.TryGetValue(key, out List<ProductResponse> cache))
            {
                Console.WriteLine("[CACHE-HIT]");
                return cache;
            }
            Console.WriteLine("[CACHE-MISS]");
            var data = await _innerService.GetAllProductAsync(request);
            _cache.Set(key, data, TimeSpan.FromMinutes(5));
            return data;
        }

        public async Task<ProductResponse?> GetProductByIdAsync(Guid id)
        {
            string key = $"p_{id}";
            if (_cache.TryGetValue(key, out ProductResponse cacheProduct))
            {
                Console.WriteLine("[CACHE-HIT]");
                return cacheProduct;
            }
            Console.WriteLine("CACHE-MISS");
            var data = await _innerService.GetProductByIdAsync(id);
            _cache.Set(key, data, TimeSpan.FromMinutes(5));
            return data;
        }
    }
}