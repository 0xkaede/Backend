using MongoDB.Bson.Serialization.Attributes;

namespace KaedeBackend.Models.Profiles.Attributes
{
    public class CosmeticLockerAttributes
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

        [BsonElement("item_seen")]
        public bool ItemSeen { get; set; }

        [BsonElement("favorite")]
        public bool Favorite { get; set; }
    }

    public class LockerSlotsData
    {
        [BsonElement("slots")]
        public Dictionary<string, Slot> Slots { get; set; }
    }

    public class Slot
    {
        [BsonElement("items")]
        public List<string> Items { get; set; }

        [BsonElement("activeVariants")]
        public List<object> ActiveVariants { get; set; }
    }
}
