using HandmadeShop.Domain.Commons;

namespace HandmadeShop.Domain.Entities
{
    public class SubImage : BaseEntity
    {
        public string Url { get; set; } = string.Empty;
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}