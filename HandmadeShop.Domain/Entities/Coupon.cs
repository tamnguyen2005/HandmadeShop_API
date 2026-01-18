using HandmadeShop.Domain.Commons;

namespace HandmadeShop.Domain.Entities
{
    public class Coupon : BaseEntity
    {
        public string Code { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public decimal? MinOrderAmount { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int UsageLimit { get; set; }
        public int UsageCount { get; set; }
        public bool IsActive { get; set; } = true;
    }
}