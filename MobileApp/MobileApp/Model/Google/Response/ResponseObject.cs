using Newtonsoft.Json;

namespace MobileApp.Model.Google.Response
{
    internal class ResponseObject
    {
        [JsonProperty("responses")]
        public AnnotateImageResponse[] Responses { get; set; }
    }
}
