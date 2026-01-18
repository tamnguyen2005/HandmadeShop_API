using HandmadeShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HandmadeShop.Infrastructure.Configuration
{
    public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            // Ten bang
            builder.ToTable("Coupon");
            // Khoa chinh
            builder.HasKey(c => c.Id);
            // Thuoc tinh
            builder.Property(c => c.Code).IsRequired();
            builder.Property(c => c.Type).IsRequired();
            builder.Property(c => c.Value).IsRequired();
            builder.Property(c => c.ExpiryDate).IsRequired();
            builder.HasIndex(c => c.Code).IsUnique();
            builder.Property(c => c.Value).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(c => c.MinOrderAmount).HasColumnType("decimal(18,2)").IsRequired();
        }
    }
}