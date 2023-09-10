using MongoDB.Bson.Serialization.Attributes;

namespace KaedeBackend.Models.Profiles
{
    [BsonIgnoreExtraElements]
    public class ProfilesMongo
    {
        [BsonElement("accountId")]
        public string AccountId { get; set; }

        [BsonElement("athena")]
        public AthenaProfile AthenaProfile { get; set; }

        [BsonElement("common_core")]
        public CommonCoreProfile CommonCoreProfile { get; set; }
    }
}
