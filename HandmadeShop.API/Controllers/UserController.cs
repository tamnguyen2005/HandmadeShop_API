using HandmadeShop.Application.DTOs.User;
using HandmadeShop.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var token = await _userService.LoginAsync(request);
            return Ok(token);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] RegisterRequest request)
        {
            await _userService.RegisterAsync(request);
            return NoContent();
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
        {
            await _userService.ForgotPassword(request);
            return NoContent();
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            await _userService.ResetPassword(request);
            return NoContent();
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            await _userService.ChangePassword(request);
            return NoContent();
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateInformation(UpdateUserRequest request)
        {
            await _userService.UpdateUserInfo(request);
            return NoContent();
        }

        [HttpDelete()]
        public async Task<IActionResult> DeleteUser(DeleteUserRequest request)
        {
            await _userService.DeleteAccount(request);
            return NoContent();
        }
    }
}