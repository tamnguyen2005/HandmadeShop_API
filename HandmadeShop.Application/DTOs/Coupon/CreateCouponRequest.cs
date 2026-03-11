using System.ComponentModel.DataAnnotations;

namespace HandmadeShop.Application.DTOs.Coupon
{
    public class CreateCouponRequest
    {
        [Required]
        public string Code { get; set; } = string.Empty;

        [Required]
        public string Type { get; set; } = string.Empty;

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Value { get; set; }

        public decimal? MinOrderAmount { get; set; }

        [Required]
        public int ExpireAfter { get; set; }

        public int UsageLimit { get; set; }
    }
}