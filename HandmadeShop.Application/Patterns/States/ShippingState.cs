using HandmadeShop.Domain.Entities;

namespace HandmadeShop.Application.Patterns.States
{
    public class ShippingState : OrderState
    {
        public override void Complete(Order order)
        {
            order.Status = "Completed";
        }
    }
}