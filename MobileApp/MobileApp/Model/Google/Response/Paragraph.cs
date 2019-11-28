using Newtonsoft.Json;

namespace MobileApp.Model.Google.Response
{
    internal class Paragraph
    {
        [JsonProperty("boundingBox")]
        public BoundingPoly BoundingBox { get; set; }

        [JsonProperty("words")]
        public Word[] Words { get; set; }

        [JsonProperty("confidence")]
        public float Confidence { get; set; }
    }
}
