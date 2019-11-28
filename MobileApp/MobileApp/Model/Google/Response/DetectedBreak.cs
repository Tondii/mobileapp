using Newtonsoft.Json;

namespace MobileApp.Model.Google.Response
{
    internal class DetectedBreak
    {
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
