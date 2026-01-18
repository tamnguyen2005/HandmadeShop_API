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

        //public string ImageURL { get; set; } = string.Empty;
        [Required]
        public Guid CategoryId { get; set; }

        public List<CreateProductOptionDTO> Options { get; set; } = new List<CreateProductOptionDTO>();
    }
}