using System.Collections.Generic;
using System.Linq;
using MobileApp.Database;
using MobileApp.Database.DTO;

namespace MobileApp.Services
{
    class DataService : IDataService
    {
        private readonly SqliteDbContext _context;

        public DataService(string dbPath)
        {
            _context = new SqliteDbContext(dbPath);
        }

        public IEnumerable<Receipt> GetAllReceipts()
        {
            return _context.Receipts.ToList();
        }

        public Receipt GetReceipt(int id)
        {
            return _context.Receipts.FirstOrDefault(r => r.Id == id);
        }

        public int AddReceipt(Receipt receipt)
        {
            _context.Receipts.Add(receipt);
            _context.SaveChanges();
            return receipt.Id;
        }

        public void DeleteReceipt(int id)
        {
            var receipt = GetReceipt(id);

            if (receipt == null) return;

            _context.Receipts.Remove(receipt);
            _context.SaveChanges();
        }
    }
}
