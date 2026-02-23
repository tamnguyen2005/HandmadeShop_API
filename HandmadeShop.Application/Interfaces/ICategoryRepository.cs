using HandmadeShop.Application.DTOs.Category;
using HandmadeShop.Domain.Entities;

namespace HandmadeShop.Application.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<DetailCategoryResponse> GetCategoryByIdAsync(Guid id);
    }
}