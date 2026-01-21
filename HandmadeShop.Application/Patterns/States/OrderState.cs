using HandmadeShop.Domain.Entities;

namespace HandmadeShop.Application.Patterns.States
{
    public abstract class OrderState
    {
        public virtual void Confirm(Order order) => throw new InvalidOperationException($"Can not 'Confirm' when order is in status '{order.Status}'");

        public virtual void Ship(Order order) => throw new InvalidOperationException($"Can not 'Ship' when order is in status '{order.Status}'");

        public virtual void Complete(Order order) => throw new InvalidOperationException($"Can not 'Complete' when order is in status '{order.Status}'");

        public virtual void Cancel(Order order) => throw new InvalidOperationException($"Can not 'Cancel' when order is in status '{order.Status}'");
    }
}