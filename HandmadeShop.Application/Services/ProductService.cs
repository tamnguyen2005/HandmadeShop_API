using HandmadeShop.Application.DTOs.Product;
using HandmadeShop.Application.Interfaces;
using HandmadeShop.Application.Patterns.Builders;
using System.Text.Json;

namespace HandmadeShop.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPhotoService _photoService;

        public ProductService(IUnitOfWork unitOfWork, IPhotoService photoService)
        {
            _unitOfWork = unitOfWork;
            _photoService = photoService;
        }

        public async Task CreateProductAsync(CreateProductRequest request)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(request.CategoryId);
            if (category == null)
            {
                throw new KeyNotFoundException("Category does not exist !");
            }
            var builder = new ProductBuilder();
            builder.AddName(request.Name)
                   .AddDescription(request.Description)
                   .AddBasePrice(request.BasePrice)
                   .AddStockQuantity(request.StockQuantity)
                   .AddStoryBehind(request.StoryBehind)
                   .AddCategoryId(category.Id);
            var options = new List<CreateProductOptionRequest>();
            try
            {
                foreach (var i in request.Options)
                {
                    var tmp = i[1..(i.Length - 1)];
                    options.Add(JsonSerializer.Deserialize<CreateProductOptionRequest>(tmp));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            foreach (var o in options)
            {
                builder.AddOption(o.Name, o.Values);
            }
            if (request.ImageURL != null && request.ImageURL.Length > 0)
            {
                var url = await _photoService.UploadAsync(request.ImageURL);
                builder.AddImageURL(url);
            }
            var product = builder.Build();
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<ProductResponse>> GetAllProductAsync(QueryProductRequest request)
        {
            var products = await _unitOfWork.Products.GetAllProductAsync(request);
            var result = products.Select(
                p => new ProductResponse()
                {
                    Id = p.Id,
                    Name = p.Name,
                    //Description = p.Description,
                    BasePrice = p.BasePrice,
                    ImageURL = p.ImageURL,
                }).ToList();
            return result;
        }

        public async Task<ProductResponse?> GetProductByIdAsync(Guid id)
        {
            var product = await _unitOfWork.Products.GetProductWithDetailAsync(id);
            if (product == null)
                throw new KeyNotFoundException("Product does not exist !");
            return new ProductResponse()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                BasePrice = product.BasePrice,
                //StockQuantity = product.StockQuantity,
                ImageURL = product.ImageURL,
                CategoryName = product.Category.Name,
                StoryBehind = product.StoryBehind,
                //ReviewCount = product.ReviewCount,
                //AverageReview = product.AverageRating,
                //Reviews = product.Reviews
                //.Select(r => new ReviewResponse()
                //{
                //    Rating = r.Rating,
                //    Content = r.Content,
                //    UserName = r.UserName,
                //    ImageURL = r.ImageURL ?? null
                //}).ToList(),
                Options = product.Options
                .Select(o => new ProductOptionResponse()
                {
                    Name = o.Name,
                    Values = o.Values.Select(v => v.Value).ToList()
                }).ToList()
            };
        }

        public async Task UpdateProductAsync(Guid id, UpdateProductRequest request)
        {
            var url = string.Empty;
            if (request.ImageURL != null && request.ImageURL.Length > 0)
                url = await _photoService.UploadAsync(request.ImageURL);
            await _unitOfWork.Products.UpdateProductAsync(id, url, request);
        }

        public async Task DeleteProduct(Guid id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
                throw new KeyNotFoundException("Product does not exist !");
            product.IsDeleted = true;
            _unitOfWork.Products.Update(product);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}