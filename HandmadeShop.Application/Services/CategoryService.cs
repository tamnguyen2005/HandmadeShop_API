using HandmadeShop.Application.DTOs.Category;
using HandmadeShop.Application.Interfaces;
using HandmadeShop.Domain.Entities;

namespace HandmadeShop.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPhotoService _photoService;

        public CategoryService(IUnitOfWork unitOfWork, IPhotoService photoService)
        {
            _unitOfWork = unitOfWork;
            _photoService = photoService;
        }

        public async Task AddCategoryAsync(CreateCategoryRequest request)
        {
            Category category = new Category()
            {
                Name = request.Name,
                Description = request.Description ?? string.Empty,
                IsCollection = request.IsCollection
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
            if (request.Image != null && request.Image.Length > 0)
            {
                var imageURL = await _photoService.UploadAsync(request.Image);
                category.ImageURL = imageURL;
            }
            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<CategoryResponse>> GetAllCategoryAsync()
        {
            var categories = await _unitOfWork.Categories.GetAllCategoryAsync();
            var result = categories.Where(c => c.IsDeleted == false).Select(c => new CategoryResponse()
            {
                Id = c.Id,
                Name = c.Name,
                ImageURL = c.ImageURL,
            }).ToList();
            return result;
        }

        public async Task<DetailCategoryResponse> GetCategoryByIdAsync(Guid id)
        {
            var category = await _unitOfWork.Categories.GetCategoryByIdAsync(id);
            if (category == null || category.IsDeleted)
                throw new KeyNotFoundException("Category does not exist !");
            var result = new DetailCategoryResponse()
            {
                Id = category.Id,
                Name = category.Name,
                ImageURL = category.ImageURL,
                SubCategory = category.SubCategories == null
                        ? new List<CategoryResponse>()
                        : category.SubCategories.Where(s => s.IsDeleted == false).Select(s => new CategoryResponse()
                        {
                            Id = s.Id,
                            Name = s.Name,
                            ImageURL = s.ImageURL,
                        })
                        .ToList(),
            };
            return result;
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

        public async Task<List<CategoryResponse>> GetAllCollectionAsync()
        {
            var collections = await _unitOfWork.Categories.GetALlCollectionAsync();
            var response = collections.Select(c => new CategoryResponse()
            {
                Id = c.Id,
                Name = c.Name,
                ImageURL = c.ImageURL,
            }).ToList();
            return response;
        }
    }
}