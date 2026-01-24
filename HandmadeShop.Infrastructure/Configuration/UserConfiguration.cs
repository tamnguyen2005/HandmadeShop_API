using HandmadeShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HandmadeShop.Infrastructure.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            // Ten bang
            builder.ToTable("User");
            // Khoa chinh
            builder.HasKey(u => u.Id);
            // Thuoc tinh
            builder.Property(u => u.Email).IsRequired();
            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.PasswordHash).IsRequired();
            builder.Property(u => u.FullName).HasMaxLength(100).IsRequired();
        }
    }
}