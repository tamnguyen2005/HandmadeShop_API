namespace HandmadeShop.Application.DTOs.Category
{
    public class DetailCategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<CategoryResponse> SubCategory { get; set; } = new List<CategoryResponse>();
        public List<MiniProductResponse> Products { get; set; } = new List<MiniProductResponse>();
    }

    public class MiniProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal BasePrice { get; set; }
        public string ImageURL { get; set; } = string.Empty;
    }
}