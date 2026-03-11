using HandmadeShop.Application.Interfaces;
using HandmadeShop.Application.Patterns.Singleton;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace HandmadeShop.Infrastructure.SignalR
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly UserConnectionManager _userConnectionManager;
        private readonly IUnitOfWork _unitOfWork;

        public ChatHub(UserConnectionManager userConnectionManager, IUnitOfWork unitOfWork)
        {
            _userConnectionManager = userConnectionManager;
            _unitOfWork = unitOfWork;
        }

        public async Task SendToAdmin(string message)
        {
            var admins = await _unitOfWork.Users.FindAsync(u => u.Role == "admin");
            var admin = admins.Select(a => a.Id.ToString()).ToList();
            if (!admin.Any())
            {
                // Khách hàng gửi tới nhưng admin đang không kết nối --> Gửi ngược lại người gửi
                await Clients.Caller.SendAsync("AdminAwayMessage", "Admin đang bận, sẽ phản hồi bạn sau !");
            }
            else
            {
                // Khách hàng gửi tin tới admin -> Admin gọi on("ReceiveMessage") để nhận thông báo
                await Clients.Clients(admin).SendAsync("ReceiveMessage", Context.User?.FindFirstValue(ClaimTypes.NameIdentifier), Context.User?.FindFirstValue(ClaimTypes.Email), message);
            }
        }

        public async Task SendByAdmin(string Id, string message)
        {
            var user = _userConnectionManager.GetConnection(Id);
            if (!user.Any())
            {
                // Admin gửi nhưng user đi đâu mất tiêu -> Gửi lại cho admin on("FailToSendMessage")
                await Clients.Caller.SendAsync("FailToSendMessage", "Người nhận đi đâu mất rồi !");
            }
            else
            {
                // Admin gửi đi thành công -> Người nhận gọi on("ChatReceiveMessage")
                await Clients.Clients(user).SendAsync("ChatReceiveMessage", message);
            }
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