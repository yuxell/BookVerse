using BookWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BookWebApp.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookPublisher> BookPublishers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<FavoriteBook> FavoriteBooks { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Çoka çok ilişki tanımlama
            builder.Entity<BookPublisher>()
                .HasOne(b => b.Book)
                .WithMany(bp => bp.BookPublishers)
                .HasForeignKey(bi => bi.BookID);
            builder.Entity<BookPublisher>()
                .HasOne(b => b.Publisher)
                .WithMany(bp => bp.BookPublishers)
                .HasForeignKey(ai => ai.PublisherID);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int> { UserId = 1, RoleId = 1 }
                );

        }
    }
}
