using HandmadeShop.Application.DTOs.User;
using HandmadeShop.Application.Interfaces;
using HandmadeShop.Domain.Entities;

namespace HandmadeShop.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateUserAsync(CreateUserRequest request)
        {
            var user = new AppUser()
            {
                FullName = request.FullName,
                Email = request.Email,
                Address = request.Address ?? "",
                PasswordHash = request.Password,
            };
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}