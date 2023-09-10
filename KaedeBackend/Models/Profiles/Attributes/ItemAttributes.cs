using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace KaedeBackend.Models.Profiles.Attributes
{
    public class ItemAttributes //skunky bozo
    {
        [BsonElement("locker_slots_data"), BsonIgnoreIfNull(), JsonProperty("locker_slots_data", NullValueHandling = NullValueHandling.Ignore)]
        public LockerSlotsData LockerSlotsData { get; set; }

        [BsonElement("use_count"), BsonIgnoreIfNull(), JsonProperty("use_count", NullValueHandling = NullValueHandling.Ignore)]
        public int UseCount { get; set; }

        [BsonElement("banner_icon_template"), BsonIgnoreIfNull(), JsonProperty("banner_icon_template", NullValueHandling = NullValueHandling.Ignore)]
        public string BannerIconTemplate { get; set; }

        [BsonElement("banner_color_template"), BsonIgnoreIfNull(), JsonProperty("banner_color_template", NullValueHandling = NullValueHandling.Ignore)]
        public string BannerColorTemplate { get; set; }

        [BsonElement("locker_name"), BsonIgnoreIfNull(), JsonProperty("locker_name", NullValueHandling = NullValueHandling.Ignore)]
        public string LockerName { get; set; }

        [BsonElement("variants"), BsonIgnoreIfNull(), JsonProperty("variants", NullValueHandling = NullValueHandling.Ignore)]
        public List<Variant> Variants { get; set; }

        [BsonElement("max_level_bonus"), BsonIgnoreIfNull(), JsonProperty("max_level_bonus", NullValueHandling = NullValueHandling.Ignore)]
        public int MaxLevelBonus { get; set; }

        [BsonElement("level"), BsonIgnoreIfNull(), JsonProperty("level", NullValueHandling = NullValueHandling.Ignore)]
        public int Level { get; set; }

        [BsonElement("item_seen"), BsonIgnoreIfNull(), JsonProperty("item_seen", NullValueHandling = NullValueHandling.Ignore)]
        public bool ItemSeen { get; set; }

        [BsonElement("xp"), BsonIgnoreIfNull(), JsonProperty("xp", NullValueHandling = NullValueHandling.Ignore)]
        public int XP { get; set; }

        [BsonElement("favorite"), BsonIgnoreIfNull(), JsonProperty("favorite", NullValueHandling = NullValueHandling.Ignore)]
        public bool Favorite { get; set; }
    }

    public class Variant
    {
        [BsonElement("channel"), JsonProperty("channel")]
        public string Channel { get; set; }

        [BsonElement("active"), JsonProperty("active")]
        public string Active { get; set; }

        [BsonElement("owned"), JsonProperty("owned")]
        public List<string> Owned { get; set; }
    }
}
