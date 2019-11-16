using System.Threading.Tasks;
using MobileApp.Database.DTO;
using MobileApp.Services;

namespace MobileApp.DocumentRecognition
{
    class ReceiptRecognizer
    {
        private readonly Receipt _receipt;
        public ReceiptRecognizer()
        {

        }

        public async Task<string> GetRecognizedText(string base64)
        {
            var requestService = new RequestService();
            return await requestService.GetRecognizedWords(base64);
        }

        public Receipt RecognizeSignature()
        {
            return new Receipt();
        }
    }
}
