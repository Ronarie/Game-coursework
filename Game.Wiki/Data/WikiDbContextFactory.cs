using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Game.Wiki.Data
{
    public class WikiDbContextFactory : IDesignTimeDbContextFactory<WikiDbContext>
    {
        public WikiDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WikiDbContext>();

            optionsBuilder.UseMySql(
                "server=localhost;database=Quest_DB;user=root;password=varyvary",
                new MySqlServerVersion(new Version(8, 0, 36))
            );

            return new WikiDbContext(optionsBuilder.Options);
        }
    }
}
