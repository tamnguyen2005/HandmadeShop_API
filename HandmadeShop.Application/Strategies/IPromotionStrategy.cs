namespace HandmadeShop.Application.Strategies
{
    public interface IPromotionStrategy
    {
        decimal CaculateDiscount(decimal originalPrice);
    }
}