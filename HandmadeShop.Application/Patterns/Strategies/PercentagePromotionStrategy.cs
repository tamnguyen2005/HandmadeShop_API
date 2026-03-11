namespace HandmadeShop.Application.Patterns.Strategies
{
    public class PercentagePromotionStrategy : IPromotionStrategy
    {
        private readonly decimal _percenttage;

        public PercentagePromotionStrategy(decimal percenttage)
        {
            _percenttage = percenttage;
        }

        public decimal CaculateDiscount(decimal originalPrice)
        {
            return (_percenttage * originalPrice) / 100;
        }
    }
}