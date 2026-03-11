namespace HandmadeShop.Application.DTOs.Coupon
{
    public class CouponResponse
    {
        public Guid Id { get; set; }
        public decimal Value { get; set; }
        public decimal MinOrderAmount { get; set; }
    }
}