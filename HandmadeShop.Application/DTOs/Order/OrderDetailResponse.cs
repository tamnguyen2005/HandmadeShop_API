namespace HandmadeShop.Application.DTOs.Order
{
    public class OrderDetailResponse
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }
        public string ShippingAddress { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public List<MiniProductResponse> Products { get; set; } = new List<MiniProductResponse>();
    }

    public class MiniProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string Configurations { get; set; } = string.Empty;
    }
}