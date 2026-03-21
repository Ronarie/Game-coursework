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
                .HasMany(l => l.Item)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Item)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Item>()
                .HasOne(p => p.Location)
                .WithOne(a => a.Type)
                .HasForeignKey(p => p.LocationName)
                .HasForeignKey(p => p.TypeName)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Item>().HasIndex(c => c.ItemUid).IsUnique();
            modelBuilder.Entity<Location>().HasIndex(q => q.LocationUid).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(a => a.TypeUid).IsUnique();
        }
    }
}
