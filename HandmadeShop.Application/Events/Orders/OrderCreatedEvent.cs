using HandmadeShop.Application.Patterns.Observers;
using HandmadeShop.Domain.Entities;

namespace HandmadeShop.Application.Events.Orders
{
    public class OrderCreatedEvent : IDomainEvent
    {
        public Order Order { get; set; }

        public OrderCreatedEvent(Order order)
        {
            Order = order;
        }
    }
}