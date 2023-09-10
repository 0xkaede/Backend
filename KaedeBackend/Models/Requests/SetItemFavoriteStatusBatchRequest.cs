using Newtonsoft.Json;

namespace KaedeBackend.Models.Requests
{
    public class SetItemFavoriteStatusBatchRequest
    {
        [JsonProperty("itemFavStatus")]
        public bool[] ItemFavStatus { get; set; }

        [JsonProperty("itemIds")]
        public string[] ItemIds { get; set; }
    }

    public class MarkItemSeenReq
    {
        [JsonProperty("itemIds")]
        public string[] ItemIds { get; set; }
    }
}
