using MobileApp.Database.DTO;

namespace MobileApp.Services
{
    class ReceiptService : IReceiptService
    {
        private readonly Receipt _receipt;
        public ReceiptService()
        {
        }

        public Receipt RecognizeSignature()
        {
            return new Receipt();
        }
    }
}
