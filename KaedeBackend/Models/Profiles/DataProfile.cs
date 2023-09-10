using MongoDB.Bson.Serialization.Attributes;

namespace KaedeBackend.Models.Profiles
{
    [BsonIgnoreExtraElements]
    public class UserProfile
    {
        [BsonElement("accountId")]
        public string AccountId { get; set; }

        [BsonElement("discordId")]
        public string discordId { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }

        [BsonElement("created")]
        public string Created { get; set; }

        [BsonElement("banned")]
        public bool banned { get; set; }

        [BsonElement("vbucks")]
        public int VBucks { get; set; }
    }
}
