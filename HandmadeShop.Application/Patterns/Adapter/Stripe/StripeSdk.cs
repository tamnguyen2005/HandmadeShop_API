namespace HandmadeShop.Application.Patterns.Adapter.Stripe
{
    public class StripeSdk
    {
        public void SendPayment(decimal dollar)
        {
            Console.WriteLine($"[STRIPE] Processing credit card payment: {dollar}...");
        }
    }
}