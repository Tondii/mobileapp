using Microsoft.EntityFrameworkCore;

namespace MobileApp.Database
{
    public class SqliteDbContext : DbContext
    {
        private readonly string _dbPath;
        public SqliteDbContext(string dbPath) : base()
        {
            _dbPath = dbPath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_dbPath);
        }

        
    }
}
