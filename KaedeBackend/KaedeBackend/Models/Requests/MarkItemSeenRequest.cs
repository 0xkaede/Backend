using Newtonsoft.Json;

namespace KaedeBackend.Models.Requests
{
    public class MarkItemSeenRequest
    {
        [JsonProperty("itemIds")]
        public string[] ItemIds { get; set; }
    }
}
