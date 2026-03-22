using Microsoft.EntityFrameworkCore;
using Game.Wiki.Models;

namespace Game.Wiki.Data
{
    public class WikiDbContext : DbContext
    {
        public WikiDbContext(DbContextOptions<WikiDbContext> options) : base(options) { }

        public DbSet<Item> Item => Set<Item>();
        public DbSet<Location> Location => Set<Location>();
        public DbSet<Category> Type => Set<Category>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Location>()
                .HasMany(l => l.Items)
                .WithOne(i => i.Location)
                .HasForeignKey(i => i.LocationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Items)
                .WithOne(i => i.Category)
                .HasForeignKey(i => i.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Item>()
                .HasOne(p => p.Location)
                .WithMany()
                .HasForeignKey(p => p.Location)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Item>()
                .HasOne(a => a.Category)
                .WithMany()
                .HasForeignKey(p => p.Category)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Item>().HasIndex(c => c.ItemUid).IsUnique();
            modelBuilder.Entity<Location>().HasIndex(q => q.LocationUid).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(a => a.TypeUid).IsUnique();
        }
    }
}
