using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MobileApp.Extensions
{
    internal static class HttpExtensions
    {
        public static async Task<HttpResponseMessage> PostAsyncAsJson(this HttpClient httpClient, string uri, object content)
        {
            var jsonContent = JsonConvert.SerializeObject(content);
            return await httpClient.PostAsync(uri, new StringContent(jsonContent, Encoding.UTF8));
        }

        public static async Task<T> ReadAsAsync<T>(this HttpContent httpContent)
        {
            var contentString = await httpContent.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(contentString);
        }
    }
}
