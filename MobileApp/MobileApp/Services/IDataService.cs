using System.Collections.Generic;
using System.Threading.Tasks;
using MobileApp.Database.DTO;

namespace MobileApp.Services
{
    public interface IDataService
    {
        IEnumerable<Receipt> GetAllReceipts();
        Receipt GetReceipt(long id);
        long AddReceipt(Receipt receipt);
        Task<long> AddReceiptAsync(Receipt receipt);
        void DeleteReceipt(long id);
        Task DeleteReceiptAsync(long id);
        void UpdateReceipt(Receipt receipt);
        Task UpdateReceiptAsync(Receipt receipt);
    }
}
