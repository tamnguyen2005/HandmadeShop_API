namespace HandmadeShop.Application.DTOs.Order
{
    public class CreateOrderRequest
    {
        public Guid UserId { get; set; }
        public string? Address { get; set; }
        public string? CouponCode { get; set; }
        public List<CreateOrderItemRequest> Items { get; set; } = new List<CreateOrderItemRequest>();
        public string PaymentMethod { get; set; } = string.Empty;
    }
}