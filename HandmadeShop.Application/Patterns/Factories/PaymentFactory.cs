using HandmadeShop.Application.Patterns.Adapter;
using HandmadeShop.Application.Patterns.Adapter.Momo;
using HandmadeShop.Application.Patterns.Adapter.Stripe;

namespace HandmadeShop.Application.Patterns.Factories
{
    public static class PaymentFactory
    {
        public static IPaymentService GetPaymentMethod(string method)
        {
            method = method.ToLower();
            switch (method)
            {
                case "momo": return new MomoAdapter();
                case "stripe": return new StripeAdapter();
                default: throw new Exception("Only Momo and Stripe payment method !");
            }
        }
    }
}