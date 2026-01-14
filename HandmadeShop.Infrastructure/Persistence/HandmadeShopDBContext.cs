using HandmadeShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HandmadeShop.Infrastructure.Persistence
{
    public class HandmadeShopDBContext : DbContext
    {
        public HandmadeShopDBContext(DbContextOptions<HandmadeShopDBContext> options) : base(options)
        {
        }

        // User
        public DbSet<AppUser> Users { get; set; }

        // Category
        public DbSet<Category> Categories { get; set; }

        // Product
        public DbSet<Product> Products { get; set; }

        public DbSet<ProductOption> ProductOptions { get; set; }
        public DbSet<ProductOptionValue> ProductOptionValues { get; set; }

        // Order
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderHistory> OrderHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HandmadeShopDBContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}