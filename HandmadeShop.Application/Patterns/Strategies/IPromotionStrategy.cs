namespace HandmadeShop.Application.Patterns.Strategies
{
    public interface IPromotionStrategy
    {
        decimal CaculateDiscount(decimal originalPrice);
    }
}