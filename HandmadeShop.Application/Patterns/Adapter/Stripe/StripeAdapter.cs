namespace HandmadeShop.Application.Patterns.Adapter.Stripe
{
    public class StripeAdapter : IPaymentService
    {
        private readonly StripeSdk _stripeSdk;

        public StripeAdapter()
        {
            _stripeSdk = new StripeSdk();
        }

        public Task<bool> PayAsync(decimal amount, string orderInfo)
        {
            decimal dollars = amount / 24000;
            _stripeSdk.SendPayment(dollars);
            Console.WriteLine($"Stripe transaction is successfull for order: {orderInfo}");
            return Task.FromResult(true);
        }
    }
}