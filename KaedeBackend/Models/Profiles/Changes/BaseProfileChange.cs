using Newtonsoft.Json;

namespace KaedeBackend.Models.Profiles.Changes
{
    public class BaseProfileChange
    {
        [JsonProperty("changeType")]
        public string ChangeType { get; set; }
    }
}
