using HandmadeShop.Application.Patterns.States;

namespace HandmadeShop.Application.Patterns.Factories
{
    public static class OrderStateFactory
    {
        public static OrderState GetState(string state)
        {
            return state switch
            {
                "Pending" => new PendingState(),
                "Confirmed" => new ConfirmedState(),
                "Shipping" => new ShippingState(),
                "Completed" => new CompletedState(),
                "Cancelled" => new CancelledState(),
                _ => throw new Exception($"{state} is not valid !")
            };
        }
    }
}