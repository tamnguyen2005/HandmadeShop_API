namespace HandmadeShop.Application.DTOs.Product
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public int StockQuantity { get; set; }
        public string ImageURL { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public List<ProductOptionResponse> Options { get; set; } = new List<ProductOptionResponse>();
    }
}