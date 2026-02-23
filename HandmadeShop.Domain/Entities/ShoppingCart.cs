namespace HandmadeShop.Domain.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; } = string.Empty;
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public ShoppingCart()
        { }

        public ShoppingCart(string userName)
        {
            UserName = userName;
        }

        public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
    }
}