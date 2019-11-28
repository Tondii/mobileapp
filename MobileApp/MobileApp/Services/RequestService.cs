using System.Net.Http;
using System.Threading.Tasks;
using MobileApp.Extensions;
using MobileApp.Model.Google.Request;
using MobileApp.Model.Google.Response;

namespace MobileApp.Services
{
    class RequestService : IRequestService
    {
        private const string ApiKey = "AIzaSyAuMegsulFeaPi86zxWDBd1qO90U0UsoB8";
        private const string GoogleUri = "https://vision.googleapis.com/v1/images:annotate";

        public async Task<ResponseObject> GetRecognizedWords(string base64)
        {
            var combinedUri = $"{GoogleUri}?key={ApiKey}";
            var request = new RequestObject(base64);
            using var httpClient = new HttpClient();
            var response = await httpClient.PostAsyncAsJson(combinedUri, request);
            return await response.Content.ReadAsAsync<ResponseObject>();
        }
    }
}
