namespace HandmadeShop.Application.Patterns.Strategies
{
    internal class FixedAmountPromotionStrategy : IPromotionStrategy
    {
        private readonly decimal _amount;

        public FixedAmountPromotionStrategy(decimal amount)
        {
            _amount = amount;
        }

        public decimal CaculateDiscount(decimal originalPrice)
        {
            return _amount > originalPrice ? originalPrice : _amount;
        }
    }
}