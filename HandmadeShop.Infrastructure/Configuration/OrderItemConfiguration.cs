using HandmadeShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HandmadeShop.Infrastructure.Configuration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            // Ten bang
            builder.ToTable("OrderItem");
            // Khoa chinh
            builder.HasKey(x => x.Id);
            // Thuoc tinh
            builder.Property(i => i.UnitPrice).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(i => i.Configuration).IsRequired(false);
            builder.Property(i => i.ProductName).IsRequired();
            // Khoa ngoai
            builder.HasOne(i => i.Product)
                   .WithMany()
                   .HasForeignKey(i => i.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}