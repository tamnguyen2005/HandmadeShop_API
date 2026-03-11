namespace HandmadeShop.Application.DTOs.User
{
    public class LoginResponse
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string AvatarURL { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}