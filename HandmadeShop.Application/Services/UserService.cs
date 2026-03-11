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
        private readonly ICurrentUserService _currentUser;

        public UserService(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IPhotoService photoService, IJwtTokenGenerator jwtTokenGenerator, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _photoService = photoService;
            _jwtTokenGenerator = jwtTokenGenerator;
            _currentUser = currentUser;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var userList = await _unitOfWork.Users.FindAsync(u => u.Email == request.Email);
            var user = userList.FirstOrDefault();
            if (user == null || _passwordHasher.Verify(user.PasswordHash, request.Password))
            {
                throw new ArgumentException("Email or password is not correct !");
            }
            return new LoginResponse()
            {
                FullName = user.FullName,
                Email = user.Email,
                AvatarURL = user.AvatarURL ?? "",
                Token = _jwtTokenGenerator.GenerateToken(user)
            };
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

        public async Task UpdateUserInfo(UpdateUserRequest request)
        {
            var userIdString = _currentUser.UserId;
            var userId = Guid.Parse(userIdString);
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User does not exist !");
            }
            if (!string.IsNullOrEmpty(request.FullName))
                user.FullName = request.FullName;
            if (!string.IsNullOrEmpty(request.Address))
                user.Address = request.Address;
            if (!string.IsNullOrEmpty(request.PhoneNumber))
                user.PhoneNumber = request.PhoneNumber;
            if (!string.IsNullOrEmpty(request.Email))
                user.Email = request.Email;
            if (request.Avartar != null && request.Avartar.Length > 0)
            {
                var url = await _photoService.UploadAsync(request.Avartar);
                user.AvatarURL = url;
            }
            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}