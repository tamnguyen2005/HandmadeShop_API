using System.ComponentModel.DataAnnotations;

namespace HandmadeShop.Application.DTOs.User
{
    public class ForgotPasswordRequest
    {
        [Required]
        public string Email { get; set; } = string.Empty;
    }
}