using HandmadeShop.Application.Interfaces;
using HandmadeShop.Application.Patterns.Singleton;
using HandmadeShop.Infrastructure.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace HandmadeShop.Infrastructure.Provider
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hub;
        private readonly UserConnectionManager _connectionManager;

        public NotificationService(IHubContext<NotificationHub> hub, UserConnectionManager userConnectionManager)
        {
            _hub = hub;
            _connectionManager = userConnectionManager;
        }

        public async Task SendNotificationToUserAsync(string userId, string message)
        {
            var connections = _connectionManager.GetInstance(userId);
            if (connections.Any())
            {
                await _hub.Clients.Clients(connections).SendAsync("OrderCreatedMessage", message);
                Console.WriteLine($"[SIGNALR] Message were sent to user {userId}");
            }
        }
    }
}