using System;
using Microsoft.EntityFrameworkCore;
using MobileApp.Database.DTO;

namespace MobileApp.Database
{
    public sealed class SqliteDbContext : DbContext
    {
        private readonly string _dbPath;
        public SqliteDbContext(string dbPath)
        {
            _dbPath = dbPath;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_dbPath}");
        }

        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Company> Companies { get; set; }

    }
}
