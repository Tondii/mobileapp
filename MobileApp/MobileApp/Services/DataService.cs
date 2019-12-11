using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MobileApp.Database;
using MobileApp.Database.DTO;

namespace MobileApp.Services
{
    class DataService : IDataService
    {
        private readonly SqliteDbContext _context;

        public DataService()
        {
            _context = SqliteContextFactory.CreateDbContext();
        }

        public IEnumerable<Receipt> GetAllReceipts()
        {
            return _context.Receipts.Include(r => r.Company).ToList();
        }

        public Receipt GetReceipt(long id)
        {
            return _context.Receipts.Include(r => r.Company).FirstOrDefault(r => r.Id == id);
        }

        public long AddReceipt(Receipt receipt)
        {
            _context.Receipts.Add(receipt);
            _context.SaveChanges();
            return receipt.Id;
        }

        public async Task<long> AddReceiptAsync(Receipt receipt)
        {
            _context.Receipts.Add(receipt);
            await _context.SaveChangesAsync();
            return receipt.Id;
        }

        public void DeleteReceipt(long id)
        {
            var receipt = GetReceipt(id);

            if (receipt == null) return;

            _context.Receipts.Remove(receipt);
            _context.SaveChanges();
        }

        public async Task DeleteReceiptAsync(long id)
        {
            var receipt = GetReceipt(id);

            if (receipt == null) return;

            _context.Receipts.Remove(receipt);
            await _context.SaveChangesAsync();
        }

        public void UpdateReceipt(Receipt receipt)
        {
            _context.Receipts.Update(receipt);
            _context.SaveChanges();
        }

        public async Task UpdateReceiptAsync(Receipt receipt)
        {
            _context.Receipts.Update(receipt);
            await _context.SaveChangesAsync();
        }
    }
}
