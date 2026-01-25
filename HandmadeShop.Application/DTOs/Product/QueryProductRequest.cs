namespace HandmadeShop.Application.DTOs.Product
{
    public class QueryProductRequest
    {
        public string? Name { get; set; }
        public decimal? BasePrice { get; set; }
        public Guid? CategoryId { get; set; }
        public int? PageSize { get; set; } = 10;
        public int? PageNumber { get; set; } = 1;
    }
}