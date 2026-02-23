using HandmadeShop.Application.Events.Orders;
using HandmadeShop.Application.Interfaces;
using HandmadeShop.Application.Patterns.Observers;

namespace HandmadeShop.Application.Features.Notification
{
    public class NotificationHandler : IHandmadeObserver<OrderCreatedEvent>
    {
        private readonly INotificationService _notiService;

        public NotificationHandler(INotificationService notiService)
        {
            _notiService = notiService;
        }

        public async Task HandleAsync(OrderCreatedEvent domainEvent)
        {
            var order = domainEvent.Order;
            var message = $"Order #{order.Id} with {order.TotalAmount} has been created !";
            await _notiService.SendNotificationToUserAsync(order.UserId.ToString(), message);
        }
    }
}