using Newtonsoft.Json;

namespace MobileApp.Model.Google.Response
{
    internal class TextAnnotation
    {
        [JsonProperty("pages")]
        public Page[] Pages { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
