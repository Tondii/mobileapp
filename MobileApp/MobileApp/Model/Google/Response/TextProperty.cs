using Newtonsoft.Json;

namespace MobileApp.Model.Google.Response
{
    internal class TextProperty
    {
        [JsonProperty("detectedLanguages")]
        public DetectedLanguage[] DetectedLanguages { get; set; }

        [JsonProperty("detectedBreak")]
        public DetectedBreak DetectedBreak { get; set; }
    }
}
