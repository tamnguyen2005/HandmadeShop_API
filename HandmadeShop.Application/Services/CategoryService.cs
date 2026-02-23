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

        public async Task<List<CategoryResponse>> GetAllCategoryAsync()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();
            var result = categories.Where(c => c.IsDeleted == false).Select(c => new CategoryResponse()
            {
                Id = c.Id,
                Name = c.Name,
            }).ToList();
            return result;
        }

        public async Task<DetailCategoryResponse> GetCategoryByIdAsync(Guid id)
        {
            return await _unitOfWork.Categories.GetCategoryByIdAsync(id);
        }

        public async Task UpdateCategoryAsync(Guid id, UpdateCategoryRequest request)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null)
                throw new KeyNotFoundException("Category does not exist !");
            if (!string.IsNullOrEmpty(request.Name))
            {
                category.Name = request.Name;
            }
            if (!string.IsNullOrEmpty(request.Description))
            {
                category.Description = request.Description;
            }
            _unitOfWork.Categories.Update(category);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null)
                throw new KeyNotFoundException("Category does not exist !");
            category.IsDeleted = true;
            _unitOfWork.Categories.Update(category);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}