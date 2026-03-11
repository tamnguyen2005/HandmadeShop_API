namespace HandmadeShop.Application.DTOs.Coupon
{
    public class UpdateCouponRequest
    {
        public string? Code { get; set; }
        public string? Type { get; set; }
        public decimal? Value { get; set; }
        public decimal? MinOrderAmount { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int? UsageLimit { get; set; }
    }
}