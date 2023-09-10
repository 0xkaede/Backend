using Newtonsoft.Json;

namespace KaedeBackend.Models.Profiles.Changes
{
    public class StatModified : BaseProfileChange
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public object Value { get; set; }
    }
}
