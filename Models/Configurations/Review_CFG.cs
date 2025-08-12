using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookWebApp.Models.Configurations
{
    public class Review_CFG : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.Property(x => x.Comment)
                .HasColumnType("varchar")
                .HasMaxLength(200)
                .IsRequired(false);
            builder.Property(x => x.Rating)
                .HasColumnType("smallint")
                .HasDefaultValue(0)
                .IsRequired();
            builder.Property(x => x.CreatedAt)
               .HasColumnType("smalldatetime")
               .IsRequired();
        }
    }
}
