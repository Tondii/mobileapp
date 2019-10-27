using System.Collections.Generic;
using MobileApp.Database.DTO;

namespace MobileApp.Services
{
    public interface IDataService
    {
        IEnumerable<Receipt> GetAllReceipts();
        Receipt GetReceipt(int id);
        int AddReceipt(Receipt receipt);
        void DeleteReceipt(int id);
    }
}
