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
    }
}