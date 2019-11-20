using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MobileApp.Services
{
    class RequestService : IRequestService
    {
        private readonly string _apiKey = "AIzaSyAuMegsulFeaPi86zxWDBd1qO90U0UsoB8";
        private string _googleUri = "https://vision.googleapis.com/v1/images:annotate";

        public async Task<string> GetRecognizedWords(string base64)
        {
            var combinedUri = $"{_googleUri}?key={_apiKey}";
            var request = new Model.Google.Request(base64);
            var response = await PostAsyncAsJson(combinedUri, request);
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<HttpResponseMessage> PostAsyncAsJson(string uri, object content)
        {
            var jsonContent = JsonConvert.SerializeObject(content);
            using var httpClient = new HttpClient();
            return await httpClient.PostAsync(uri, new StringContent(jsonContent, Encoding.UTF8));
        }
    }
}
