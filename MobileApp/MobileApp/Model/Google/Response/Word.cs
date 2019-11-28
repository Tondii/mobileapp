using Newtonsoft.Json;

namespace MobileApp.Model.Google.Response
{
    internal class Word
    {
        [JsonProperty("property")]
        public TextProperty Property { get; set; }

        [JsonProperty("boundingBox")]
        public BoundingPoly BoundingBox { get; set; }

        [JsonProperty("symbols")]
        public Symbol[] Symbols { get; set; }

        [JsonProperty("confidence")]
        public float Confidence { get; set; }
    }
}
