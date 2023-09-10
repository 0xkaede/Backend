using MongoDB.Bson.Serialization.Attributes;

namespace KaedeBackend.Models.Profiles.Attributes
{
    public class ItemAttributes //skunky bozo
    {
        [BsonElement("locker_slots_data")]
        public LockerSlotsData LockerSlotsData { get; set; }

        [BsonElement("use_count")]
        public int UseCount { get; set; }

        [BsonElement("banner_icon_template")]
        public string BannerIconTemplate { get; set; }

        [BsonElement("banner_color_template")]
        public string BannerColorTemplate { get; set; }

        [BsonElement("locker_name")]
        public string LockerName { get; set; }

        [BsonElement("variants")]
        public List<Variant> Variants { get; set; }

        [BsonElement("max_level_bonus")]
        public int MaxLevelBonus { get; set; }

        [BsonElement("level")]
        public int Level { get; set; }

        [BsonElement("item_seen")]
        public bool ItemSeen { get; set; }

        [BsonElement("xp")]
        public int XP { get; set; }

        [BsonElement("favorite")]
        public bool Favorite { get; set; }
    }

    public class Variant
    {
        [BsonElement("channel")]
        public string Channel { get; set; }

        [BsonElement("active")]
        public string Active { get; set; }

        [BsonElement("owned")]
        public List<string> Owned { get; set; }
    }
}
