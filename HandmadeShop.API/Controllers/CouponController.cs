using HandmadeShop.Application.DTOs.Coupon;
using HandmadeShop.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CouponController : ControllerBase
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCoupon()
        {
            return Ok(await _couponService.GetCoupons());
        }

        [HttpPost]
        public async Task<IActionResult> AddCoupon(CreateCouponRequest request)
        {
            await _couponService.AddNewCoupon(request);
            return StatusCode(StatusCodes.Status201Created, new { message = "Create new coupon successfully !" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCoupon([FromRoute] Guid id, [FromBody] UpdateCouponRequest request)
        {
            await _couponService.UpdateCoupon(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoupon(Guid id)
        {
            await _couponService.DeleteCoupon(id);
            return NoContent();
        }
    }
}