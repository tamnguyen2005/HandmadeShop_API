namespace HandmadeShop.Domain.Entities
{
    public class CartItem
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Option { get; set; }
        public string ImageURL { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}