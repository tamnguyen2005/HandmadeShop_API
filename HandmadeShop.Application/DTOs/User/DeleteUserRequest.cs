using System.ComponentModel.DataAnnotations;

namespace HandmadeShop.Application.DTOs.User
{
    public class DeleteUserRequest
    {
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}