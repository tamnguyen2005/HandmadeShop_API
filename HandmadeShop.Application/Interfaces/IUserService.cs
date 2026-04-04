using HandmadeShop.Application.DTOs.User;

namespace HandmadeShop.Application.Interfaces
{
    public interface IUserService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);

        Task RegisterAsync(RegisterRequest request);

        Task UpdateUserInfo(UpdateUserRequest request);

        Task ForgotPassword(ForgotPasswordRequest request);

        Task ResetPassword(ResetPasswordRequest request);

        Task ChangePassword(ChangePasswordRequest request);

        Task DeleteAccount(DeleteUserRequest request);
    }
}