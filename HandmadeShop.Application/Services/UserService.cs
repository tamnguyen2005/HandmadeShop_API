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
        private readonly ICacheService _cache;
        private readonly IEmailService _mail;

        public UserService(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IPhotoService photoService, IJwtTokenGenerator jwtTokenGenerator, ICurrentUserService currentUser, ICacheService cache, IEmailService mail)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _photoService = photoService;
            _jwtTokenGenerator = jwtTokenGenerator;
            _currentUser = currentUser;
            _cache = cache;
            _mail = mail;
        }

        public async Task ChangePassword(ChangePasswordRequest request)
        {
            var userIdString = _currentUser.UserId;
            if (string.IsNullOrEmpty(userIdString))
            {
                throw new UnauthorizedAccessException("Please log in first !");
            }
            var userId = Guid.Parse(userIdString);
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User does not exist !");
            }
            if (!_passwordHasher.Verify(user.PasswordHash, request.OldPassword))
            {
                throw new Exception("Old password does not correct !");
            }
            if (request.NewPassword != request.ConfirmPassword)
            {
                throw new Exception("New password and confirm does not match !");
            }
            var newHashedPassword = _passwordHasher.Hash(request.NewPassword);
            user.PasswordHash = newHashedPassword;
            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAccount(DeleteUserRequest request)
        {
            var userIdString = _currentUser.UserId;
            if (string.IsNullOrEmpty(userIdString))
            {
                throw new UnauthorizedAccessException("Please log in first !");
            }
            var userId = Guid.Parse(userIdString);
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User does not exist !");
            }
            if (!_passwordHasher.Verify(user.PasswordHash, request.Password))
            {
                throw new Exception("Password does not correct !");
            }
            _unitOfWork.Users.Delete(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task ForgotPassword(ForgotPasswordRequest request)
        {
            var users = await _unitOfWork.Users.FindAsync(u => u.Email == request.Email);
            var user = users.FirstOrDefault();
            if (user == null)
            {
                throw new KeyNotFoundException("Email does not exist !");
            }
            Random rand = new Random();
            string randomNumber = string.Concat(Enumerable.Range(0, 6)
                .Select(_ => rand.Next(0, 10)));
            var key = $"ResetOTP_{request.Email}";
            string subject = "Atelier - Mã OTP xác thực của bạn";
            string body = $@"
<div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #e0e0e0; border-radius: 10px;'>
    <h2 style='color: #2c3e50; text-align: center;'>Handmade Shop</h2>
    <p style='font-size: 16px; color: #333;'>Chào bạn,</p>
    <p style='font-size: 16px; color: #333;'>Bạn vừa yêu cầu đặt lại mật khẩu cho tài khoản của mình. Dưới đây là mã xác thực (OTP) của bạn:</p>

    <div style='text-align: center; margin: 30px 0;'>
        <span style='font-size: 32px; font-weight: bold; color: #e74c3c; letter-spacing: 5px; padding: 10px 20px; background-color: #f9ecec; border-radius: 5px;'>
            {randomNumber}
        </span>
    </div>

    <p style='font-size: 16px; color: #333;'>Mã OTP này sẽ hết hạn sau <strong>3 phút</strong>.</p>
    <p style='font-size: 14px; color: #7f8c8d; margin-top: 30px; border-top: 1px solid #eee; padding-top: 15px;'>
        * Nếu bạn không yêu cầu đổi mật khẩu, vui lòng bỏ qua email này. Tài khoản của bạn vẫn an toàn.
    </p>
</div>";
            await _cache.SetAsync(key, randomNumber, TimeSpan.FromMinutes(3));
            await _mail.SendEmailAsync(request.Email, subject, body);
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var userList = await _unitOfWork.Users.FindAsync(u => u.Email == request.Email);
            var user = userList.FirstOrDefault();
            if (user == null || !_passwordHasher.Verify(user.PasswordHash, request.Password))
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

        public async Task ResetPassword(ResetPasswordRequest request)
        {
            var key = $"ResetOTP_{request.Email}";
            var otp = await _cache.GetAsync<string>(key);
            if (string.IsNullOrEmpty(otp))
            {
                throw new Exception("OTP ran out of time !");
            }
            if (otp != request.Otp)
            {
                throw new Exception("OTP does not match !");
            }
            var newHashedPassword = _passwordHasher.Hash(request.Password);
            var users = await _unitOfWork.Users.FindAsync(u => u.Email == request.Email);
            var user = users.FirstOrDefault();
            user.PasswordHash = newHashedPassword;
            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();
            await _cache.RemoveAsync(key);
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