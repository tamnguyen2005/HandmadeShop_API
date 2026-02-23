using HandmadeShop.Application.Patterns.Singleton;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace HandmadeShop.Infrastructure.SignalR
{
    public class NotificationHub : Hub
    {
        private readonly UserConnectionManager _userConnectionManager;

        public NotificationHub(UserConnectionManager userConnectionManager)
        {
            _userConnectionManager = userConnectionManager;
        }

        public override Task OnConnectedAsync()
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                _userConnectionManager.KeepUserConnection(userId, Context.ConnectionId);
                Console.WriteLine($"[SIGNALR] User {userId} has connected (ID: {Context.ConnectionId})");
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            _userConnectionManager.RemoveUserConnection(Context.ConnectionId);
            Console.WriteLine($"[SIGNALR] Connection {Context.ConnectionId} disconnected");
            return base.OnDisconnectedAsync(exception);
        }
    }
}