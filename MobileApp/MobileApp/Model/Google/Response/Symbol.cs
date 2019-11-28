using Newtonsoft.Json;

namespace MobileApp.Model.Google.Response
{
    internal class Symbol
    {
        [JsonProperty("property")]
        public TextProperty Property { get; set; }

        [JsonProperty("boundingBox")]
        public BoundingPoly BoundingBox { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("confidence")]
        public float Confidence { get; set; }
    }
}
