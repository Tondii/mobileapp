using System.Collections.Generic;
using Newtonsoft.Json;

namespace MobileApp.Model.Google.Request
{
    internal class RequestObject
    {
        [JsonProperty("requests")]
        public List<AnnotateImageRequest> Requests { get; }

        public RequestObject(string base64)
        {
            var features = new List<Feature>()
            {
                new Feature()
            };

            Requests = new List<AnnotateImageRequest>
            {
                new AnnotateImageRequest
                {
                    Features = features,
                    Image = new Image(base64)
                }
            };
        }
    }
}
