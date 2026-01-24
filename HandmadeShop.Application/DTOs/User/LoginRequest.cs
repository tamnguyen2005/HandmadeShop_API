using System.ComponentModel.DataAnnotations;

namespace HandmadeShop.Application.DTOs.User
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Password { get; set; } = string.Empty;
    }
}