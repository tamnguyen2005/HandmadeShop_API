using HandmadeShop.Domain.Entities;

namespace HandmadeShop.Application.Patterns.States
{
    public class PendingState : OrderState
    {
        public override void Confirm(Order order)
        {
            order.Status = "Confirmed";
        }

        public override void Cancel(Order order)
        {
            order.Status = "Canceled";
        }
    }
}