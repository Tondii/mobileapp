using Newtonsoft.Json;

namespace MobileApp.Model.Google.Request
{
    internal class Feature
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        public Feature(string type = "DOCUMENT_TEXT_DETECTION")
        {
            Type = type;
        }
    }
}
