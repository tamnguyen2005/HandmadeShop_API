namespace HandmadeShop.Application.DTOs.Product
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? StoryBehind { get; set; }
        public decimal BasePrice { get; set; }
        public int? StockQuantity { get; set; }
        public string ImageURL { get; set; } = string.Empty;

        //public int? ReviewCount { get; set; } = 0;
        //public double? AverageReview { get; set; } = 0;
        public string? CategoryName { get; set; } = string.Empty;

        //public List<ReviewResponse> Reviews { get; set; } = new List<ReviewResponse>();
        public List<ProductOptionResponse> Options { get; set; } = new List<ProductOptionResponse>();
    }
}