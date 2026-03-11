using HandmadeShop.Domain.Entities;

namespace HandmadeShop.Application.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category?> GetCategoryByIdAsync(Guid id);

        Task<List<Category>> GetALlCollectionAsync();

        Task<List<Category>> GetAllCategoryAsync();
    }
}