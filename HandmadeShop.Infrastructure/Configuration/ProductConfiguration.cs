using HandmadeShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HandmadeShop.Infrastructure.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Ten bang
            builder.ToTable("Product");
            // Khoa chinh
            builder.HasKey(p => p.Id);
            // Thuoc tinh
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Property(p => p.StoryBehind).IsRequired();
            builder.Property(p => p.BasePrice).HasColumnType("decimal(18,2)").IsRequired();
            // Khoa ngoai
            builder.HasOne(p => p.Category)
                   .WithMany(c => c.Products)
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}