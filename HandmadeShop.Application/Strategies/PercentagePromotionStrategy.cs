namespace HandmadeShop.Application.Strategies
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
            return _percenttage * originalPrice;
        }
    }
}