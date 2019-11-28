using System.Collections.Generic;
using Newtonsoft.Json;

namespace MobileApp.Model.Google.Request
{
    internal class AnnotateImageRequest
    {
        [JsonProperty("image")]
        public Image Image { get; set; }

        [JsonProperty("features")]
        public IEnumerable<Feature> Features { get; set; }

        [JsonProperty("imageContext")]
        public ImageContext ImageContext { get; set; }
    }
}
