namespace HandmadeShop.Application.DTOs.Review
{
    public class ReviewEvent
    {
        public Guid ProductId { get; set; }
        public double Rating { get; set; }

        public ReviewEvent(Guid productId, double rating)
        {
            this.ProductId = productId;
            this.Rating = rating;
        }
    }
}