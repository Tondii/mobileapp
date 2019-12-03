using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MobileApp.Database
{
    public class SqliteContextFactory : IDesignTimeDbContextFactory<SqliteDbContext>
    {
        public static SqliteDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<SqliteDbContext>();
            optionsBuilder.UseSqlite($"Filename={App.DbPath}");

            return new SqliteDbContext(optionsBuilder.Options);
        }

        public SqliteDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SqliteDbContext>();
            optionsBuilder.UseSqlite($"Filename={App.DbPath}");

            return new SqliteDbContext(optionsBuilder.Options);
        }
    }
}
