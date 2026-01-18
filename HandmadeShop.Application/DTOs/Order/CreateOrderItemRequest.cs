namespace HandmadeShop.Application.DTOs.Order
{
    public class CreateOrderItemRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public string Configuration { get; set; } = string.Empty;
    }
}