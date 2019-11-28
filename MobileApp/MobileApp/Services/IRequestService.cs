using System.Threading.Tasks;
using MobileApp.Model.Google.Response;

namespace MobileApp.Services
{
    internal interface IRequestService
    {
        Task<ResponseObject> GetRecognizedWords(string base64);
    }
}
