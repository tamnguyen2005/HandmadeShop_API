using HandmadeShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HandmadeShop.Infrastructure.Configuration
{
    public class SubImageConfiguration : IEntityTypeConfiguration<SubImage>
    {
        public void Configure(EntityTypeBuilder<SubImage> builder)
        {
            builder.ToTable("SubImage");
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Product)
                   .WithMany(p => p.SubImages)
                   .HasForeignKey(x => x.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}