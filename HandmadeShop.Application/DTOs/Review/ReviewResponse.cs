namespace HandmadeShop.Application.DTOs.Review
{
    public class ReviewResponse
    {
        public double Rating { get; set; }
        public string Content { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string? ImageURL { get; set; }
    }
}