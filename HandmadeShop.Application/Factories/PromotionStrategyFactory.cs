using HandmadeShop.Application.Strategies;
using HandmadeShop.Domain.Entities;

namespace HandmadeShop.Application.Factories
{
    public static class PromotionStrategyFactory
    {
        public static IPromotionStrategy? Create(Coupon coupon)
        {
            if (!coupon.IsActive) return null;
            if (coupon.ExpiryDate.HasValue && coupon.ExpiryDate < DateTime.UtcNow) return null;
            if (coupon.UsageCount >= coupon.UsageLimit) return null;
            return coupon.Type switch
            {
                "Percentage" => new PercentagePromotionStrategy(coupon.Value),
                "Fixed" => new FixedAmountPromotionStrategy(coupon.Value),
                _ => null
            };
        }
    }
}