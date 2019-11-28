using Microsoft.EntityFrameworkCore;

namespace MobileApp.Database
{
    public class SqliteContextFactory
    {
        public static SqliteDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<SqliteDbContext>();
            optionsBuilder.UseSqlite($"Filename={App.DbPath}");

            return new SqliteDbContext(optionsBuilder.Options);
        }
    }
}
