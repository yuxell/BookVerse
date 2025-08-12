using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using BookWebApp.Models;

namespace BookWebApp.Models.Configurations
{
    public class FavoriteBook_CFG : IEntityTypeConfiguration<FavoriteBook>
    {
        public void Configure(EntityTypeBuilder<FavoriteBook> builder)
        {
            builder.HasIndex(fb => new { fb.UserID, fb.BookID })
                    .IsUnique();
            // Aynı kullanıcı aynı kitabı favorilere tekrar ekleyememesi için (Unique Constraint)
        }
    }
}
