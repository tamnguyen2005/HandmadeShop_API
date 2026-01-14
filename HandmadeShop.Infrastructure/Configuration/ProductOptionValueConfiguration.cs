using HandmadeShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HandmadeShop.Infrastructure.Configuration
{
    public class ProductOptionValueConfiguration : IEntityTypeConfiguration<ProductOptionValue>
    {
        public void Configure(EntityTypeBuilder<ProductOptionValue> builder)
        {
            // Ten bang
            builder.ToTable("ProductOptionValue");
            // Khoa chinh
            builder.HasKey(pov => pov.Id);
            // Thuoc tinh
            builder.Property(pov => pov.Value).IsRequired();
            builder.Property(pov => pov.AdditionalPrice).HasColumnType("decimal(18,2)");
        }
    }
}