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

        [HttpPost]
        public async Task<IActionResult> AddNewUser(CreateUserRequest request)
        {
            await _userService.CreateUserAsync(request);
            return StatusCode(StatusCodes.Status201Created, new { message = "Create new user successfully !" });
        }
    }
}