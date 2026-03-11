using Microsoft.AspNetCore.Http;

namespace HandmadeShop.Application.DTOs.User
{
    public class UpdateUserRequest
    {
        public string? FullName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public IFormFile? Avartar { get; set; }
    }
}