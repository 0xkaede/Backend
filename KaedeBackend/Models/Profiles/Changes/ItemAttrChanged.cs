using Newtonsoft.Json;

namespace KaedeBackend.Models.Profiles.Changes
{
    public class ItemAttrChanged : BaseProfileChange
    {
        [JsonProperty("itemId")]
        public string ItemId { get; set; }

        [JsonProperty("attributeName")]
        public string AttributeName { get; set; }

        [JsonProperty("attributeValue")]
        public object AttributeValue { get; set; }
    }
}
