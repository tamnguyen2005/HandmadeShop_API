using HandmadeShop.Application.DTOs.User;

namespace HandmadeShop.Application.Interfaces
{
    public interface IUserService
    {
        Task CreateUserAsync(CreateUserRequest request);
    }
}