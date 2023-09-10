using Newtonsoft.Json;

namespace KaedeBackend.Models.Requests
{
    public class SetBattleRoyaleBannerRequest
    {
        [JsonProperty("homebaseBannerIconId")]
        public string HomebaseBannerIconId { get; set; }

        [JsonProperty("homebaseBannerColorId")]
        public string HomebaseBannerColorId { get; set; }
    }
}
