using HandmadeShop.Application.DTOs.Category;
using HandmadeShop.Application.Interfaces;
using HandmadeShop.Domain.Entities;

namespace HandmadeShop.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddCategoryAsync(CreateCategoryRequest request)
        {
            Category category = new Category()
            {
                Name = request.Name,
                Description = request.Description ?? string.Empty,
            };
            if (request.ParentId.HasValue)
            {
                var parent = await _unitOfWork.Categories.GetByIdAsync(request.ParentId.Value);
                if (parent == null)
                {
                    throw new KeyNotFoundException("ParentId does not exist !");
                }
                category.ParentId = request.ParentId;
            }
            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}