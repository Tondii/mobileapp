using Newtonsoft.Json;

namespace MobileApp.Model.Google.Response
{
    internal class Block
    {
        [JsonProperty("boundingBox")]
        public BoundingPoly BoundingBox { get; set; }

        [JsonProperty("paragraphs")]
        public Paragraph[] Paragraphs { get; set; }

        [JsonProperty("blockType")]
        public string BlockType { get; set; }

        [JsonProperty("confidence")]
        public float Confidence { get; set; }
    }
}
