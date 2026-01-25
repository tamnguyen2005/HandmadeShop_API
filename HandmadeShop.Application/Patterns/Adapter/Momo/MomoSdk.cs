namespace HandmadeShop.Application.Patterns.Adapter.Momo
{
    public class MomoSdk
    {
        public void ProcessMomoPayment(double vndAmount)
        {
            Console.WriteLine($"[MOMO] Paying {vndAmount} VND... ");
        }
    }
}