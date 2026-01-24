using HandmadeShop.Application.DTOs.User;

namespace HandmadeShop.Application.Interfaces
{
    public interface IUserService
    {
        Task<string> LoginAsync(LoginRequest request);

        Task RegisterAsync(RegisterRequest request);
    }
}