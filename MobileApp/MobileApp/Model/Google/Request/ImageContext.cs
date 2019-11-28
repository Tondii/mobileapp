using System.Collections.Generic;
using Newtonsoft.Json;

namespace MobileApp.Model.Google.Request
{
    internal class ImageContext
    {
        [JsonProperty("languageHints")]
        public IEnumerable<string> LanguageHints { get; set; }
    }
}
