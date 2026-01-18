using System.ComponentModel.DataAnnotations;

namespace HandmadeShop.Application.DTOs.User
{
    public class CreateUserRequest
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Password { get; set; } = string.Empty;

        public string? Address { get; set; }
        //public IFormFile AvatarURL { get; set; }
    }
}