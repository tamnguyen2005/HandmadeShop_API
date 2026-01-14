using HandmadeShop.Domain.Commons;

namespace HandmadeShop.Domain.Entities
{
    public class OrderHistory : BaseEntity
    {
        public Guid OrderId { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Note { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}