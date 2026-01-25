namespace HandmadeShop.Application.Patterns.Adapter
{
    public interface IPaymentService
    {
        Task<bool> PayAsync(decimal amount, string orderInfo);
    }
}