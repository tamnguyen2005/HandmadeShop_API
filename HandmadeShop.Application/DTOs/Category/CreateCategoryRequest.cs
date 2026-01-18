using System.ComponentModel.DataAnnotations;

namespace HandmadeShop.Application.DTOs.Category
{
    public class CreateCategoryRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
        public Guid? ParentId { get; set; }
    }
}