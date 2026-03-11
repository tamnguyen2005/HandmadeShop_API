using HandmadeShop.Domain.Commons;

namespace HandmadeShop.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string ImageURL { get; set; } = string.Empty;
        public virtual Product Product { get; set; } = null!;
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string? Configuration { get; set; }
    }
}