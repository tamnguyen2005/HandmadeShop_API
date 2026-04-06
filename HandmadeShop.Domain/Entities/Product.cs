using HandmadeShop.Domain.Commons;

namespace HandmadeShop.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public int StockQuantity { get; set; }
        public string ImageURL { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;

        //public double AverageRating { get; set; } = 0;
        //public int ReviewCount { get; set; } = 0;
        public string StoryBehind { get; set; } = string.Empty;

        //public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public virtual ICollection<ProductOption> Options { get; set; } = new List<ProductOption>();

        public virtual ICollection<SubImage> SubImages { get; set; } = new List<SubImage>();
    }
}