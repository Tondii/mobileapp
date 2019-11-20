using System.Threading.Tasks;

namespace MobileApp.Services
{
    public interface IRequestService
    {
        Task<string> GetRecognizedWords(string base64);
    }
}
