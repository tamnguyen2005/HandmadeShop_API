using HandmadeShop.Application.DTOs.Coupon;

namespace HandmadeShop.Application.Interfaces
{
    public interface ICouponService
    {
        public Task AddNewCoupon(CreateCouponRequest request);

        public Task<List<CouponResponse>> GetCoupons();

        public Task UpdateCoupon(Guid Id, UpdateCouponRequest request);

        public Task DeleteCoupon(Guid Id);
    }
}