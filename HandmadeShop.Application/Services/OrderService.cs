using HandmadeShop.Application.DTOs.Order;
using HandmadeShop.Application.Events.Orders;
using HandmadeShop.Application.Interfaces;
using HandmadeShop.Application.Patterns.Adapter;
using HandmadeShop.Application.Patterns.Factories;
using HandmadeShop.Application.Patterns.Observers;
using HandmadeShop.Application.Patterns.States;
using HandmadeShop.Application.Patterns.Strategies;
using HandmadeShop.Domain.Entities;

namespace HandmadeShop.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EventDispatcher _dispatcher;
        private readonly ICurrentUserService _currentUserService;

        public OrderService(IUnitOfWork unitOfWork, EventDispatcher eventDispatcher, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _dispatcher = eventDispatcher;
            _currentUserService = currentUserService;
        }

        public async Task UpdateOrderStatusAsync(Guid id, string action)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(id);
            if (order == null)
                throw new KeyNotFoundException("Order does not exist !");
            OrderState orderState = OrderStateFactory.GetState(order.Status);
            switch (action.ToUpper())
            {
                case "CONFIRM": orderState.Confirm(order); break;
                case "SHIP": orderState.Ship(order); break;
                case "COMPLETE": orderState.Complete(order); break;
                case "CANCEL": orderState.Cancel(order); break;
                default: throw new ArgumentException("Action is invalid !");
            }
            _unitOfWork.Orders.Update(order);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CreateOrderAsync(CreateOrderRequest request)
        {
            var orderItems = new List<OrderItem>();
            decimal totalAmount = 0;
            var userIdString = _currentUserService.UserId;
            if (string.IsNullOrEmpty(userIdString))
            {
                throw new UnauthorizedAccessException("Please log in first !");
            }
            var userId = Guid.Parse(userIdString);
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("User does not exist !");
            // Tạo order item
            foreach (var i in request.Items)
            {
                var product = await _unitOfWork.Products.GetByIdAsync(i.ProductId);
                if (product == null)
                    throw new KeyNotFoundException("Product does not exist !");
                var item = new OrderItem()
                {
                    ProductId = i.ProductId,
                    ProductName = product.Name,
                    ImageURL = product.ImageURL,
                    UnitPrice = product.BasePrice,
                    Quantity = i.Quantity,
                    Configuration = i.Configuration,
                };
                var subTotal = i.Quantity * product.BasePrice;
                totalAmount += subTotal;
                orderItems.Add(item);
            }
            // Tính tiền
            var coupon = await _unitOfWork.Coupons.FindAsync(c => c.Code == request.CouponCode);
            var couponEntity = coupon.FirstOrDefault();
            if (couponEntity != null)
            {
                IPromotionStrategy strategy = PromotionStrategyFactory.Create(couponEntity);
                if (strategy != null)
                {
                    var discountAmount = strategy.CaculateDiscount(totalAmount);
                    if (discountAmount > totalAmount)
                        discountAmount = totalAmount;
                    totalAmount -= discountAmount;
                    couponEntity.UsageCount++;
                    _unitOfWork.Coupons.Update(couponEntity);
                }
            }
            // Tính tiền Tạo order mới
            var order = new Order()
            {
                UserId = user.Id,
                TotalAmount = totalAmount,
                ShippingAddress = !string.IsNullOrEmpty(request.Address) ?
                                   request.Address : user.Address ?? "Tại cửa hàng",
                PaymentMethod = request.PaymentMethod,
                Status = "Pending",
                Items = orderItems
            };
            IPaymentService _paymentService = PaymentFactory.GetPaymentMethod(request.PaymentMethod);
            bool isPaid = await _paymentService.PayAsync(totalAmount, $"{order.Id}");
            if (!isPaid)
                throw new Exception("Transaction failed !");
            await _unitOfWork.Orders.AddAsync(order);
            await _dispatcher.PublishAsync(new OrderCreatedEvent(order));
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<OrderResponse>> GetAllUserOrderAsync()
        {
            var userIdString = _currentUserService.UserId;
            if (string.IsNullOrEmpty(userIdString))
            {
                throw new UnauthorizedAccessException("Please log in first !");
            }
            var userId = Guid.Parse(userIdString);
            var orders = await _unitOfWork.Orders.FindAsync(o => o.UserId == userId && o.IsDeleted == false);
            var result = orders.Select(o => new OrderResponse()
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                PaymentMethod = o.PaymentMethod,
                Status = o.Status,
            }).ToList();
            return result;
        }

        public async Task<OrderDetailResponse> GetOrderDetailAsync(Guid id)
        {
            var order = await _unitOfWork.Orders.GetOrderByIdAsync(id);
            if (order == null)
                throw new KeyNotFoundException("Order does not exist");
            var response = new OrderDetailResponse()
            {
                Id = id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                ShippingAddress = order.ShippingAddress,
                PaymentMethod = order.PaymentMethod,
                Status = order.Status,
                Products = order.Items.Select(i => new MiniProductResponse()
                {
                    Id = i.Id,
                    Name = i.ProductName,
                    Quantity = i.Quantity,
                    ImageURL = i.ImageURL,
                    UnitPrice = i.UnitPrice,
                    Configurations = i.Configuration,
                }).ToList()
            };
            return response;
        }

        public async Task DeleteOrder(Guid id)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(id);
            if (order == null || order.IsDeleted)
                throw new KeyNotFoundException("Order does not exist !");
            order.IsDeleted = true;
            await _unitOfWork.SaveChangesAsync();
        }
    }
}