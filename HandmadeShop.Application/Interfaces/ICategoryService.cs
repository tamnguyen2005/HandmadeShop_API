using HandmadeShop.Application.DTOs.Category;

namespace HandmadeShop.Application.Interfaces
{
    public interface ICategoryService
    {
        Task AddCategoryAsync(CreateCategoryRequest request);

        Task<List<CategoryResponse>> GetAllCategoryAsync();

        Task<DetailCategoryResponse> GetCategoryByIdAsync(Guid id);

        Task UpdateCategoryAsync(Guid id, UpdateCategoryRequest request);

        Task DeleteCategoryAsync(Guid id);
    }
}