using HandmadeShop.Domain.Entities;

namespace HandmadeShop.Application.Patterns.States
{
    public class ConfirmedState : OrderState
    {
        public override void Ship(Order order)
        {
            order.Status = "Shipping";
        }

        public override void Cancel(Order order)
        {
            order.Status = "Cancelled";
        }
    }
}