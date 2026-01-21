using HandmadeShop.Application.Features.Orders.Events;
using HandmadeShop.Application.Interfaces;
using HandmadeShop.Application.Patterns.Observers;

namespace HandmadeShop.Application.Features.Inventory
{
    public class InventoryHandler : IHandmadeObserver<OrderCreatedEvent>
    {
        private readonly IUnitOfWork _unitOfWork;

        public InventoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(OrderCreatedEvent domainEvent)
        {
            var order = domainEvent.Order;
            foreach (var item in order.Items)
            {
                var product = await _unitOfWork.Products.GetByIdAsync(item.ProductId);
                if (product != null)
                {
                    product.StockQuantity -= item.Quantity;
                    if (product.StockQuantity < 0)
                    {
                        throw new Exception($"{product.Name} does not enough in the inventory !");
                    }
                    _unitOfWork.Products.Update(product);
                }
            }
        }
    }
}