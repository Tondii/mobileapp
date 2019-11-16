using System.Collections.Generic;
using System.Threading.Tasks;
using MobileApp.Database.DTO;

namespace MobileApp.Services
{
    public interface IDataService
    {
        IEnumerable<Receipt> GetAllReceipts();
        Receipt GetReceipt(int id);
        int AddReceipt(Receipt receipt);
        Task<int> AddReceiptAsync(Receipt receipt);
        void DeleteReceipt(int id);
        Task DeleteReceiptAsync(int id);
        void UpdateReceipt(Receipt receipt);
        Task UpdateReceiptAsync(Receipt receipt);
    }
}
