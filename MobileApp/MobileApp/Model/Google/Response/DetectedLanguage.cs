﻿using Newtonsoft.Json;

namespace MobileApp.Model.Google.Response
{
    internal class DetectedLanguage
    {
        [JsonProperty("languageCode")]
        public string LanguageCode { get; set; }

        [JsonProperty("confidence")]
        public float Confidence { get; set; }
    }
}
