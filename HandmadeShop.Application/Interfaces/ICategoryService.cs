using HandmadeShop.Application.DTOs.Category;

namespace HandmadeShop.Application.Interfaces
{
    public interface ICategoryService
    {
        Task AddCategoryAsync(CreateCategoryRequest request);
    }
}