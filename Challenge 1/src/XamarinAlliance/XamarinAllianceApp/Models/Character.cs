using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace XamarinAllianceApp.Models
{
    public class Character
    {
        [JsonProperty(PropertyName = "id")]
        public Int32 Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "gender")]
        public string Gender { get; set; }

        [JsonProperty(PropertyName = "biography")]
        public string Biography { get; set; }

        [JsonProperty(PropertyName = "height")]
        public float Height { get; set; }

        [JsonProperty(PropertyName = "databankUrl")]
        public string DatabankUrl { get; set; }

        [JsonProperty(PropertyName = "weapons")]
        public ICollection<Weapon> Weapons { get; set; }

        [JsonProperty(PropertyName = "appearances")]
        public ICollection<Movie> Appearances { get; set; }

        public string Version { get; set; }
    }
}
