using HandmadeShop.Domain.Commons;

namespace HandmadeShop.Domain.Entities
{
    public class ProductOption : BaseEntity
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<ProductOptionValue> Values { get; set; } = new List<ProductOptionValue>();
    }

    public class ProductOptionValue : BaseEntity
    {
        public Guid ProductOptionId { get; set; }
        public string Value { get; set; } = string.Empty;
        public decimal AdditionalPrice { get; set; }
    }
}