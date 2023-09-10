using Newtonsoft.Json;

namespace KaedeBackend.Models.Profiles.Changes
{
    public class FullProfileUpdate : BaseProfileChange
    {
        [JsonProperty("profile")]
        public object Profile { get; set; }
    }
}
