using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookWebApp.Models.Configurations
{
    public class Category_CFG : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.CategoryName)
                .HasColumnType("varchar")
                .HasMaxLength(30)
                .IsRequired();

            builder.HasData(
                new Category { CategoryID = 1, CategoryName = "Roman" },
                new Category { CategoryID = 2, CategoryName = "Hikâye (Öykü)" },
                new Category { CategoryID = 3, CategoryName = "Bilim Kurgu" },
                new Category { CategoryID = 4, CategoryName = "Fantastik" },
                new Category { CategoryID = 5, CategoryName = "Korku" },
                new Category { CategoryID = 6, CategoryName = "Gerilim / Polisiye" },
                new Category { CategoryID = 7, CategoryName = "Macera" },
                new Category { CategoryID = 8, CategoryName = "Tarihi Kurgu" },
                new Category { CategoryID = 9, CategoryName = "Distopya" },
                new Category { CategoryID = 10, CategoryName = "Gotik Kurgu" },
                new Category { CategoryID = 11, CategoryName = "Psikolojik Kurgu" },
                new Category { CategoryID = 12, CategoryName = "Dram" },
                new Category { CategoryID = 13, CategoryName = "Biyografi / Otobiyografi" },
                new Category { CategoryID = 14, CategoryName = "Anı (Hatıra)" },
                new Category { CategoryID = 15, CategoryName = "Tarih" },
                new Category { CategoryID = 16, CategoryName = "Bilim" },
                new Category { CategoryID = 17, CategoryName = "Felsefe" },
                new Category { CategoryID = 18, CategoryName = "Din" },
                new Category { CategoryID = 19, CategoryName = "Psikoloji" },
                new Category { CategoryID = 20, CategoryName = "Sosyoloji" },
                new Category { CategoryID = 21, CategoryName = "Kişisel Gelişim" },
                new Category { CategoryID = 22, CategoryName = "İş Dünyası / Ekonomi" },
                new Category { CategoryID = 23, CategoryName = "Politika" },
                new Category { CategoryID = 24, CategoryName = "Sanat" },
                new Category { CategoryID = 25, CategoryName = "Gezi" },
                new Category { CategoryID = 26, CategoryName = "Yemek / Gastronomi" },
                new Category { CategoryID = 27, CategoryName = "Çocuk Kitapları" },
                new Category { CategoryID = 28, CategoryName = "Gençlik Kitapları" },
                new Category { CategoryID = 29, CategoryName = "Eğitici Kitaplar" },
                new Category { CategoryID = 30, CategoryName = "Ders Kitapları" },
                new Category { CategoryID = 31, CategoryName = "Mühendislik" },
                new Category { CategoryID = 32, CategoryName = "Tıp" },
                new Category { CategoryID = 33, CategoryName = "Hukuk" },
                new Category { CategoryID = 34, CategoryName = "Edebiyat" },
                new Category { CategoryID = 35, CategoryName = "Dil Öğrenme" },
                new Category { CategoryID = 36, CategoryName = "Çizgi Roman" },
                new Category { CategoryID = 37, CategoryName = "Manga" },
                new Category { CategoryID = 38, CategoryName = "Mizah" }
                );
        }
    }
}
