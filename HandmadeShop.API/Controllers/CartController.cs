using HandmadeShop.Application.Interfaces;
using HandmadeShop.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ICurrentUserService _currentUser;

        public CartController(ICartService cartService, ICurrentUserService currentUser)
        {
            _cartService = cartService;
            _currentUser = currentUser;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var userName = _currentUser.UserId;
            return Ok(await _cartService.GetCartAsync(userName));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCart(ShoppingCart cart)
        {
            var userName = _currentUser.UserId;
            cart.UserName = userName;
            return Ok(await _cartService.UpdateCartAsync(cart));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCart()
        {
            var userName = _currentUser.UserId;
            await _cartService.DeleteCartAsync(userName);
            return NoContent();
        }
    }
}