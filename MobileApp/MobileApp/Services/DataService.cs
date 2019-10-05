using System;
using System.Collections.Generic;
using System.Text;
using MobileApp.Database;

namespace MobileApp.Services
{
    class DataService : IDataService
    {
        private SqliteDbContext _context;

        public DataService(string dbPath)
        {
            _context = new SqliteDbContext(dbPath);
        }
    }
}
