using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HandmadeShop.Application.DTOs.Product
{
    public class CreateProductRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0, double.MaxValue)]
        public decimal BasePrice { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        public IFormFile ImageURL { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        public string StoryBehind { get; set; }

        public List<string> Options { get; set; } = new List<string>();
        public List<IFormFile>? SubImages { get; set; }
    }
}