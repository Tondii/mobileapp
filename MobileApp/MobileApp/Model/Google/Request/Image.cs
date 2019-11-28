using Newtonsoft.Json;

namespace MobileApp.Model.Google.Request
{
    internal class Image
    {
        [JsonProperty("content")]
        public string Content { get; set; }

        public Image(string content)
        {
            Content = content;
        }
    }
}
