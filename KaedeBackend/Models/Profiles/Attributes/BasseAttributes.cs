using MongoDB.Bson.Serialization.Attributes;

namespace KaedeBackend.Models.Profiles.Attributes
{
    public class BaseAttributes
    {
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
}
