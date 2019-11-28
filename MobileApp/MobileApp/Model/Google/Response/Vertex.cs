using Newtonsoft.Json;

namespace MobileApp.Model.Google.Response
{
    internal class Vertex
    {
        [JsonProperty("x")]
        public int X { get; set; }

        [JsonProperty("y")]
        public int Y { get; set; }
    }
}
