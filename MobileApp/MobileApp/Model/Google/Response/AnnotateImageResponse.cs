using Newtonsoft.Json;

namespace MobileApp.Model.Google.Response
{
    internal class AnnotateImageResponse
    {
        [JsonProperty("textAnnotations")]
        public EntityAnnotation[] EntityAnnotations { get; set; }

        [JsonProperty("fullTextAnnotation")]
        public TextAnnotation FullTextAnnotation { get; set; }
    }
}
