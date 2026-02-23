using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HandmadeShop.Application.DTOs.Review
{
    public class CreateReviewRequest
    {
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        public Guid ProductId { get; set; }
        public IFormFile? ImageURL { get; set; }
    }
}