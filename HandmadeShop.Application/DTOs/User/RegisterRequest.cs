using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HandmadeShop.Application.DTOs.User
{
    public class RegisterRequest
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public IFormFile? AvatarURL { get; set; } = null;
    }
}