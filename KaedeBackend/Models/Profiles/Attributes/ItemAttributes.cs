using MongoDB.Bson.Serialization.Attributes;

namespace KaedeBackend.Models.Profiles.Attributes
{
    public class ItemAttributes //skunky bozo
    {
        [BsonElement("locker_slots_data"), BsonIgnoreIfNull()]
        public LockerSlotsData LockerSlotsData { get; set; }

        [BsonElement("use_count"), BsonIgnoreIfNull()]
        public int UseCount { get; set; }

        [BsonElement("banner_icon_template"), BsonIgnoreIfNull()]
        public string BannerIconTemplate { get; set; }

        [BsonElement("banner_color_template"), BsonIgnoreIfNull()]
        public string BannerColorTemplate { get; set; }

        [BsonElement("locker_name"), BsonIgnoreIfNull()]
        public string LockerName { get; set; }

        [BsonElement("variants"), BsonIgnoreIfNull()]
        public List<Variant> Variants { get; set; }

        [BsonElement("max_level_bonus"), BsonIgnoreIfNull()]
        public int MaxLevelBonus { get; set; }

        [BsonElement("level"), BsonIgnoreIfNull()]
        public int Level { get; set; }

        [BsonElement("item_seen"), BsonIgnoreIfNull()]
        public bool ItemSeen { get; set; }

        [BsonElement("xp"), BsonIgnoreIfNull()]
        public int XP { get; set; }

        [BsonElement("favorite"), BsonIgnoreIfNull()]
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
