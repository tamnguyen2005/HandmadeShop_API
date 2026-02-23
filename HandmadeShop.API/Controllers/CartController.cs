using HandmadeShop.Application.Interfaces;
using HandmadeShop.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HandmadeShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var userName = User.FindFirst(ClaimTypes.Email)?.Value;
            return Ok(await _cartService.GetCartAsync(userName));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCart(ShoppingCart cart)
        {
            var userName = User.FindFirst(ClaimTypes.Email)?.Value;
            cart.UserName = userName;
            return Ok(await _cartService.UpdateCartAsync(cart));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCart()
        {
            var userName = User.FindFirst(ClaimTypes.Email)?.Value;
            await _cartService.DeleteCartAsync(userName);
            return NoContent();
        }
    }
}