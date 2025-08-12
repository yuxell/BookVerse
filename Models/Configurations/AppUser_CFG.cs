using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookWebApp.Models.Configurations
{
    public class AppUser_CFG : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasIndex(x => x.UserName);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(30);
            builder.Property(x => x.Surname)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(30);
            builder.Property(x => x.Address)
                .HasColumnType("varchar")
                .HasMaxLength(60);

            builder.Property(x => x.PasswordHash)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(200);


            // Default Admin kullanıcısını ekler
            AppUser user = new AppUser()
            {
                Id = 1,
                Name = "Admin",
                Surname = "Context",
                Address = "Kadıköy",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@test.com",
                NormalizedEmail = "ADMIN@TEST.COM",
                EmailConfirmed = true,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                SecurityStamp = Guid.NewGuid().ToString()
            };
            // Default Admin için şifre hash'leme işlemi yapar
            PasswordHasher<AppUser> passwordHasher = new PasswordHasher<AppUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, "admin123.");
            builder.HasData(user);
        }
    }
}
