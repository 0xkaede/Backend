using KaedeBackend.Models.Profiles.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace KaedeBackend.Models.Profiles.Other
{
    public class AthenaItem
    {
        [BsonElement("accountId")]
        public string AccountId { get; set; }

        [BsonElement("items")]
        public Dictionary<string, AthenaItems> Items { get; set; }

        [BsonElement("lockers")]
        public Dictionary<string, AthenaLockers> Lockers { get; set; }
    }

    public class AthenaItems
    {
        [BsonElement("templateId")]
        public string TemplateId { get; set; }

        [BsonElement("attributes")]
        public ItemAttributes Attributes { get; set; }

        [BsonElement("quantity")]
        public int Quantity { get; set; }
    }

    public class AthenaLockers
    {
        [BsonElement("templateId")]
        public string TemplateId { get; set; }

        [BsonElement("attributes")]
        public CosmeticLockerAttributes Attributes { get; set; }

        [BsonElement("quantity")]
        public int Quantity { get; set; }
    }
}
