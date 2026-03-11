using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HandmadeShop.Application.DTOs.Category
{
    public class CreateCategoryRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public bool IsCollection { get; set; }
        public string? Description { get; set; }
        public Guid? ParentId { get; set; }
        public IFormFile? Image { get; set; }
    }
}