using Microsoft.EntityFrameworkCore;
using MobileApp.Database.DTO;

namespace MobileApp.Database
{
    public sealed class SqliteDbContext : DbContext
    {
        public SqliteDbContext(DbContextOptions<SqliteDbContext> options) : base(options)
        {
        }

        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Company> Companies { get; set; }

    }
}
