using HandmadeShop.Application.Patterns.Strategies;
using HandmadeShop.Domain.Entities;

namespace HandmadeShop.Application.Patterns.Factories
{
    public static class PromotionStrategyFactory
    {
        public static IPromotionStrategy? Create(Coupon coupon)
        {
            if (!coupon.IsActive) return null;
            if (coupon.ExpiryDate.HasValue && coupon.ExpiryDate < DateTime.UtcNow) return null;
            if (coupon.UsageCount >= coupon.UsageLimit) return null;
            coupon.Type = coupon.Type.ToLower();
            return coupon.Type switch
            {
                "percentage" => new PercentagePromotionStrategy(coupon.Value),
                "fixed" => new FixedAmountPromotionStrategy(coupon.Value),
                _ => null
            };
        }
    }
}