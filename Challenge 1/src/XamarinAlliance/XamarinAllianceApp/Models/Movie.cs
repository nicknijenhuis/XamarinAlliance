using Newtonsoft.Json;
using System;

namespace XamarinAllianceApp.Models
{
    public class Movie
    {
        [JsonProperty(PropertyName = "id")]
        public Int32 Id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        public string Version { get; set; }
    }
}
