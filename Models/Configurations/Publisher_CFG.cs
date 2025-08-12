using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookWebApp.Models.Configurations
{
    public class Publisher_CFG : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.Property(x => x.PublisherName)
               .HasColumnType("varchar")
               .HasMaxLength(60)
               .IsRequired();

            builder.HasData(
                new Publisher { PublisherID = 1, PublisherName = "Can Yayınları" },
                new Publisher { PublisherID = 2, PublisherName = "İş Bankası Yayınları" },
                new Publisher { PublisherID = 3, PublisherName = "Domingo Yayınları" },
                new Publisher { PublisherID = 4, PublisherName = "İthaki Yayınları" }
                );
        }
    }
}
