namespace HandmadeShop.Application.DTOs.Category
{
    public class DetailCategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? ImageURL { get; set; }
        public List<CategoryResponse> SubCategory { get; set; } = new List<CategoryResponse>();
    }
}