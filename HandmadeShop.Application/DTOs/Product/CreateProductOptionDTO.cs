namespace HandmadeShop.Application.DTOs.Product
{
    public class CreateProductOptionDTO
    {
        public string Name { get; set; } = string.Empty;
        public List<string> Values { get; set; } = new List<string>();
    }
}