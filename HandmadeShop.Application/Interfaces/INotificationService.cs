namespace HandmadeShop.Application.Interfaces
{
    public interface INotificationService
    {
        Task SendNotificationToUserAsync(string userId, string message);
    }
}