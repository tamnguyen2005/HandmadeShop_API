using HandmadeShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HandmadeShop.Infrastructure.Configuration
{
    public class OrderHistoryConfiguration : IEntityTypeConfiguration<OrderHistory>
    {
        public void Configure(EntityTypeBuilder<OrderHistory> builder)
        {
            // Ten bang
            builder.ToTable("OrderHistory");
            // Khoa chinh
            builder.HasKey(oh => oh.Id);
            // Thuoc tinh
            builder.Property(oh => oh.Status).HasMaxLength(50).IsRequired();
        }
    }
}