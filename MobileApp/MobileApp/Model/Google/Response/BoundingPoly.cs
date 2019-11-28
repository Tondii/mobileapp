using Newtonsoft.Json;

namespace MobileApp.Model.Google.Response
{
    internal class BoundingPoly
    {
        [JsonProperty("vertices")]
        public Vertex[] Vertices { get; set; }
    }
}
