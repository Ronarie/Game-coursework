using Game.Auth.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Game.Auth.Data
{
    public class AuthDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public void SeedAdmin()
        {
            if (!Users.Any(u => u.Role == "Admin"))
            {
                var admin = new User
                {
                    UserUid = Guid.NewGuid(),
                    Username = "admin",
                    Email = "admin@game.local",
                    PasswordHash = ComputeHash("Adm1n!"),
                    Role = "Admin"
                };

                Users.Add(admin);
                SaveChanges();
            }
        }

        private static string ComputeHash(string input)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}