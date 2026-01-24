using HandmadeShop.Application.DTOs.User;
using HandmadeShop.Application.Interfaces;
using HandmadeShop.Domain.Entities;

namespace HandmadeShop.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IPhotoService _photoService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public UserService(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IPhotoService photoService, IJwtTokenGenerator jwtTokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _photoService = photoService;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<string> LoginAsync(LoginRequest request)
        {
            var userList = await _unitOfWork.Users.FindAsync(u => u.Email == request.Email);
            var user = userList.FirstOrDefault();
            if (user == null || _passwordHasher.Verify(user.PasswordHash, request.Password))
            {
                throw new ArgumentException("Email or password is not correct !");
            }
            return _jwtTokenGenerator.GenerateToken(user);
        }

        public async Task RegisterAsync(RegisterRequest request)
        {
            var isEmailExist = await _unitOfWork.Users.FindAsync(u => u.Email == request.Email);
            if (isEmailExist.Any())
            {
                throw new ArgumentException("This email has already been used !");
            }
            var passwordHash = _passwordHasher.Hash(request.Password);
            var user = new AppUser()
            {
                Id = Guid.NewGuid(),
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = passwordHash,
                PhoneNumber = request.PhoneNumber ?? "",
                Address = request.Address ?? "",
                Role = "Customer"
            };
            if (request.AvatarURL != null)
            {
                var url = await _photoService.UploadAsync(request.AvatarURL);
                user.AvatarURL = url;
            }
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}