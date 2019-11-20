using System.Collections.Generic;
using Newtonsoft.Json;

namespace MobileApp.Model.Google
{
    class Request
    {
        [JsonProperty("requests")]
        private List<AnnotateImageRequest> Requests { get; set; }

        public Request(string base64)
        {
            var features = new List<Feature>()
            {
                new Feature()
            };

            Requests = new List<AnnotateImageRequest>
            {
                new AnnotateImageRequest
                {
                    Features = features, Image = new Image(base64), ImageContext = new ImageContext()
                }
            };
        }
    }

    internal class AnnotateImageRequest
    {
        [JsonProperty("image")]
        public Image Image { get; set; }
        [JsonProperty("features")]
        public List<Feature> Features { get; set; }
        [JsonProperty("imageContext")]
        public ImageContext ImageContext { get; set; }
    }

    internal class Image
    {
        [JsonProperty("content")]
        public string Content { get; set; }

        public Image(string content)
        {
            Content = content;
        }
    }

    internal class Feature
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        public Feature(string type = "DOCUMENT_TEXT_DETECTION")
        {
            Type = type;
        }
    }

    internal class ImageContext
    {
        [JsonProperty("languageHints")]
        public List<string> LanguageHints { get; set; }
    }
}
