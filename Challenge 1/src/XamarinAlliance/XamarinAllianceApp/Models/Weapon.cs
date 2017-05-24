using Newtonsoft.Json;

namespace XamarinAllianceApp.Models
{
    public class Weapon
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        public string Version { get; set; }
    }
}
