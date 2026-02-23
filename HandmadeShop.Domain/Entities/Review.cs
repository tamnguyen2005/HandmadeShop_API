using HandmadeShop.Domain.Commons;

namespace HandmadeShop.Domain.Entities
{
    public class Review : BaseEntity
    {
        public int Rating { get; set; }
        public string Content { get; set; } = string.Empty;
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public Product Product { get; set; }
        public AppUser User { get; set; }
        public string? ImageURL { get; set; }
    }
}