using KaedeBackend.Models.Profiles.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KaedeBackend.Models.Profiles
{
    [BsonIgnoreExtraElements]
    public class AthenaProfile
    {
        [BsonElement("created")]
        public string Created { get; set; }

        [BsonElement("updated")]
        public string Updated { get; set; }

        [BsonElement("rvn")]
        public int Revision { get; set; }

        [BsonElement("wipeNumber")]
        public int WipeNumber { get; set; }

        [BsonElement("accountId")]
        public string AccountId { get; set; }

        [BsonElement("profileId")]
        public string ProfileId { get; set; }

        [BsonElement("version")]
        public string Version { get; set; }

        [BsonElement("items")]
        public Dictionary<string, ProfileItem> Items { get; set; }

        [BsonElement("stats")]
        public AthenaStats Stats { get; set; }

        [BsonElement("commandRevision")]
        public int CommandRevision { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class ProfileItem
    {
        [BsonElement("templateId")]
        public string TemplateId { get; set; }

        [BsonElement("attributes")]
        public ItemAttributes Attributes { get; set; }

        [BsonElement("quantity")]
        public int Quantity { get; set; }
    }

    public class AthenaAtrributes
    {
        [BsonElement("favorite_character"), JsonProperty("favorite_character")]
        public string FavoriteCharacter { get; set; }

        [BsonElement("favorite_backpack"), JsonProperty("favorite_backpack")]
        public string FavoriteBackpack { get; set; }

        [BsonElement("favorite_pickaxe"), JsonProperty("favorite_pickaxe")]
        public string FavoritePickaxe { get; set; }

        [BsonElement("favorite_glider"), JsonProperty("favorite_glider")]
        public string FavoriteGlider { get; set; }

        [BsonElement("favorite_skydivecontrail"), JsonProperty("favorite_skydivecontrail")]
        public string FavoriteSkyDiveContrail { get; set; }

        [BsonElement("favorite_dance"), JsonProperty("favorite_dance")]
        public List<string> FavoriteDance { get; set; }

        [BsonElement("favorite_itemwraps"), JsonProperty("favorite_itemwraps")]
        public List<string> FavoriteItemWraps { get; set; }

        [BsonElement("favorite_loadingscreen"), JsonProperty("favorite_loadingscreen")]
        public string FavoriteLoadingScreen { get; set; }

        [BsonElement("favorite_musicpack"), JsonProperty("favorite_loadingscreen")]
        public string FavoriteMusicPack { get; set; }

        [BsonElement("use_random_loadouts")]
        public bool UseRandomLoadouts { get; set; }

        [BsonElement("banner_icon"), JsonProperty("banner_icon")]
        public string BannerIcon { get; set; }

        [BsonElement("banner_color"), JsonProperty("banner_color")]
        public string BannerColor { get; set; }

        [BsonElement("season_match_boost")]
        public int SeasonMatchBoost { get; set; }

        [BsonElement("loadouts")]
        public List<string> Loadouts { get; set; }

        [BsonElement("mfa_reward_claimed")]
        public bool MfaRewardClaimed { get; set; }

        [BsonElement("rested_xp_overflow")]
        public int RestedXpOverflow { get; set; }

        [BsonElement("quest_manager")]
        public QuestManager QuestManager { get; set; }

        [BsonElement("book_level")]
        public int BookLevel { get; set; }

        [BsonElement("season_num")]
        public int SeasonNum { get; set; }

        [BsonElement("season_update")]
        public int SeasonUpdate { get; set; }

        [BsonElement("book_xp")]
        public int BookXp { get; set; }

        [BsonElement("permissions")]
        public List<object> Permissions { get; set; }

        [BsonElement("battlestars")]
        public int BattleStars { get; set; }

        [BsonElement("battlestars_season_total")]
        public int BattleStarsSeasonTotal { get; set; }

        [BsonElement("book_purchased")]
        public bool BookPurchased { get; set; }

        [BsonElement("lifetime_wins")]
        public int LifetimeWins { get; set; }

        [BsonElement("party_assist_quest")]
        public string PartyAssistQuest { get; set; }

        [BsonElement("purchased_battle_pass_tier_offers")]
        public List<PurchasedBattlePassTierOffer> PurchasedBattlePassTierOffers { get; set; }

        [BsonElement("rested_xp_exchange")]
        public double RestedXpExchange { get; set; }

        [BsonElement("level")]
        public int Level { get; set; }

        [BsonElement("xp_overflow")]
        public int XpOverflow { get; set; }

        [BsonElement("rested_xp")]
        public int RestedXp { get; set; }

        [BsonElement("rested_xp_mult")]
        public double RestedXpMult { get; set; }

        [BsonElement("season_first_tracking_bits")]
        public List<object> SeasonFirstTrackingBits { get; set; }

        [BsonElement("accountLevel")]
        public int AccountLevel { get; set; }

        [BsonElement("competitive_identity")]
        public object CompetitiveIdentity { get; set; }

        [BsonElement("last_applied_loadout")]
        public string LastAppliedLoadout { get; set; }

        [BsonElement("daily_rewards")]
        public object DailyRewards { get; set; }

        [BsonElement("xp")]
        public int Xp { get; set; }

        [BsonElement("season_friend_match_boost")]
        public int SeasonFriendMatchBoost { get; set; }

        [BsonElement("last_match_end_datetime")]
        public DateTime LastMatchEndDateTime { get; set; }

        [BsonElement("active_loadout_index")]
        public int ActiveLoadoutIndex { get; set; }

        [BsonElement("inventory_limit_bonus")]
        public int InventoryLimitBonus { get; set; }
    }

    public class AthenaStats
    {
        [BsonElement("attributes")]
        public AthenaAtrributes Attributes { get; set; }
    }

    public class PurchasedBattlePassTierOffer
    {
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("count")]
        public int Count { get; set; }
    }

    public class QuestManager
    {
        [BsonElement("dailyLoginInterval")]
        public DateTime DailyLoginInterval { get; set; }

        [BsonElement("dailyQuestRerolls")]
        public int DailyQuestRerolls { get; set; }
    }

    public class Vote
    {
        [BsonElement("voteCount")]
        public int VoteCount { get; set; }

        [BsonElement("firstVoteAt")]
        public DateTime FirstVoteAt { get; set; }

        [BsonElement("lastVoteAt")]
        public DateTime LastVoteAt { get; set; }
    }
}