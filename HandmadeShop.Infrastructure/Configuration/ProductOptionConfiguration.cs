using HandmadeShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HandmadeShop.Infrastructure.Configuration
{
    public class ProductOptionConfiguration : IEntityTypeConfiguration<ProductOption>
    {
        public void Configure(EntityTypeBuilder<ProductOption> builder)
        {
            // Ten bang
            builder.ToTable("ProductOption");
            // Khoa chinh
            builder.HasKey(x => x.Id);
            // Thuoc tinh
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
        }
    }
}