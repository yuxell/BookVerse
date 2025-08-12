using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookWebApp.Models.Configurations
{
    public class Book_CFG : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(x => x.Title)
                .HasColumnType("varchar")
                .HasMaxLength(60)
                .IsRequired();
            builder.Property(x => x.Price)
                .HasColumnType("money")
                .IsRequired();
            builder.Property(x => x.Description)
                .HasColumnType("varchar")
                .HasMaxLength(500)
                .IsRequired();
            builder.Property(x => x.CoverImage)
                .HasColumnType("varchar")
                .HasMaxLength(200)
                .HasDefaultValue("default-book.png"); // Modelde ve burda default fotograf belirtildi. Modelde belirtilmesinin amacı: Nesne oluşturulduğunda default değer gelir. Configuration'da belirtilmesinin amacı: Database tarafında da default değer atanması için
        }
    }
}
