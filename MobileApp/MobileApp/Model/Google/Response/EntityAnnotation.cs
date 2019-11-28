using Newtonsoft.Json;

namespace MobileApp.Model.Google.Response
{
    internal class EntityAnnotation
    {
        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("boundingPoly")]
        public BoundingPoly BoundingPoly { get; set; }
    }
}
