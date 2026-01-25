namespace HandmadeShop.Application.Patterns.Adapter.Momo
{
    public class MomoAdapter : IPaymentService
    {
        private readonly MomoSdk _momoSdk;

        public MomoAdapter()
        {
            _momoSdk = new MomoSdk();
        }

        public Task<bool> PayAsync(decimal amount, string orderInfo)
        {
            double amountInDouble = (double)amount;
            _momoSdk.ProcessMomoPayment(amountInDouble);
            Console.WriteLine($"Transaction is processed completely for order :{orderInfo}");
            return Task.FromResult(true);
        }
    }
}