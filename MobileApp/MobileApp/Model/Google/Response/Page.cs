using Newtonsoft.Json;

namespace MobileApp.Model.Google.Response
{
    internal class Page
    {
        [JsonProperty("property")]
        public TextProperty Property { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("blocks")]
        public Block[] Blocks { get; set; }
    }
}
