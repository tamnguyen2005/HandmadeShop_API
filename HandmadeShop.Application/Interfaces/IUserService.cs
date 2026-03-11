using HandmadeShop.Application.DTOs.User;

namespace HandmadeShop.Application.Interfaces
{
    public interface IUserService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);

        Task RegisterAsync(RegisterRequest request);

        Task UpdateUserInfo(UpdateUserRequest request);
    }
}