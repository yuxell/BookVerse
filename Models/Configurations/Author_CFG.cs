using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookWebApp.Models.Configurations
{
    public class Author_CFG:IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(x=>x.AuthorName)
                .HasColumnType("varchar")
                .HasMaxLength(30)
                .IsRequired();
            builder.Property(x => x.AuthorSurname)
                .HasColumnType("varchar")
                .HasMaxLength(30);
            builder.Property(x=>x.AuthorDescription)
                .HasColumnType("varchar")
                .HasMaxLength(300)
                .IsRequired();

            builder.HasData(
                new Author { AuthorID = 1, AuthorName = "Martin Luther", AuthorSurname = "King", AuthorDescription = "He was an American Baptist minister, activist, and political philosopher who was one of the leading figures of the civil rights movement from 1955 until his assassination in 1968." },
                new Author { AuthorID = 2, AuthorName = "Stefan", AuthorSurname = "Zweig", AuthorDescription = "He was an Austro-Hungarian novelist, playwright, biographer, and journalist. At the peak of his literary career in the 1920s and 1930s, Zweig was one of the most translated and popular authors of his time." }
                );
        }
    }
}
