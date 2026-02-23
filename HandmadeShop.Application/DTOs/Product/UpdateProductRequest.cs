using Microsoft.AspNetCore.Http;

namespace HandmadeShop.Application.DTOs.Product
{
    public class UpdateProductRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? BasePrice { get; set; }
        public int? StockQuantity { get; set; }
        public IFormFile? ImageURL { get; set; }
        public Guid? CategoryId { get; set; }
    }
}