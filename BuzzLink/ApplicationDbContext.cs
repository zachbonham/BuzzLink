using BuzzLink.Entities;
using BuzzLink.Services;
using Microsoft.EntityFrameworkCore;

namespace BuzzLink
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options) { }

        required public DbSet<ShortenedUrl> ShortenedUrls { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShortenedUrl>(builder =>
            {
                builder.Property(s => s.Id).UseIdentityColumn();

                builder.Property(s => s.LongUrl).HasMaxLength(2048);
                builder.Property(s => s.ShortUrl).HasMaxLength(2048);

                builder.Property(s => s.Code).HasMaxLength(UrlShorteningService.NumberOfCharsInShortLink);

                builder.HasIndex(s => s.Code).IsUnique();

            });
        }
    }
}
