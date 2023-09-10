using KaedeBackend.Models.Profiles.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace KaedeBackend.Models.Profiles
{
    [BsonIgnoreExtraElements]
    public class CommonCoreProfile
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
        public Dictionary<string, CommonProfileItem> Items { get; set; }

        [BsonElement("stats")]
        public CommonCoreStats Stats { get; set; }

        [BsonElement("commandRevision")]
        public int CommandRevision { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class CommonProfileItem
    {
        [BsonElement("_t")]
        public string Id { get; set; }

        [BsonElement("templateId")]
        public string TemplateId { get; set; }

        [BsonElement("attributes")]
        public CommonItemAttributes Attributes { get; set; }

        [BsonElement("quantity")]
        public int Quantity { get; set; }
    }

    public class CommonItemAttributes
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

        [BsonElement("platform")]
        public string Platform { get; set; }
    }

    public class CommonCoreStats
    {
        [BsonElement("attributes")]
        public CommonCoreAtrributes Attributes { get; set; }
    }

    public class CommonCoreAtrributes
    {
        [BsonElement("subscriptions")]
        public object[] Subscriptions { get; set; }

        [BsonElement("personal_offers")]
        public object PersonalOffers { get; set; }

        [BsonElement("mtx_purchase_history")]
        public MtxPurchaseHistory MtxPurchaseHistory { get; set; }

        [BsonElement("import_friends_claimed")]
        public object ImportFriendsClaimed { get; set; }

        [BsonElement("current_mtx_platform")]
        public string CurrentMtxPlatform { get; set; }

        [BsonElement("mtx_affiliate")]
        public string MtxAffiliate { get; set; }

        [BsonElement("daily_purchases")]
        public Purchases DailyPurchases { get; set; }

        [BsonElement("in_app_purchases")]
        public InAppPurchases InAppPurchases { get; set; }

        [BsonElement("forced_intro_played")]
        public string ForcedIntroPlayed { get; set; }

        [BsonElement("inventory_limit_bonus")]
        public int InventoryLimitBonus { get; set; }

        [BsonElement("undo_timeout")]
        public string UndoTimeout { get; set; }

        [BsonElement("permissions")]
        public object[] Permissions { get; set; }

        [BsonElement("mfa_enabled")]
        public bool MfaEnabled { get; set; }

        [BsonElement("allowed_to_receive_gifts")]
        public bool AllowedToReceiveGifts { get; set; }

        [BsonElement("gift_history")]
        public GiftHistory GiftHistory { get; set; }

        [BsonElement("promotion_status")]
        public PromotionStatus PromotionStatus { get; set; }

        [BsonElement("survey_data")]
        public SurveyData SurveyData { get; set; }

        [BsonElement("intro_game_played")]
        public bool IntroGamePlayed { get; set; }

        [BsonElement("ban_status")]
        public BanStatus BanStatus { get; set; }

        [BsonElement("undo_cooldowns")]
        public List<UndoCooldown> UndoCooldowns { get; set; }

        [BsonElement("mtx_affiliate_set_time")]
        public string MtxAffiliateSetTime { get; set; }

        [BsonElement("weekly_purchases")]
        public Purchases WeeklyPurchases { get; set; }

        [BsonElement("ban_history")]
        public BanHistory BanHistory { get; set; }

        [BsonElement("monthly_purchases")]
        public Purchases MonthlyPurchases { get; set; }

        [BsonElement("allowed_to_send_gifts")]
        public bool AllowedToSendGifts { get; set; }

        [BsonElement("mtx_affiliate_id")]
        public string MtxAffiliateId { get; set; }
    }

    public class MtxPurchaseHistory
    {
        [BsonElement("refundsUsed")]
        public int RefundsUsed { get; set; }

        [BsonElement("refundCredits")]
        public int RefundCredits { get; set; }

        [BsonElement("purchases")]
        public List<MtxPurchase> Purchases { get; set; }
    }

    public class Purchases
    {
        [BsonElement("lastInterval")]
        public DateTime LastInterval { get; set; }

        [BsonElement("purchaseList")]
        public Dictionary<string, int> PurchaseList { get; set; }
    }

    public class InAppPurchases
    {
        [BsonElement("receipts")]
        public List<string> Receipts { get; set; }

        [BsonElement("ignoredReceipts")]
        public List<string> IgnoredReceipts { get; set; }

        [BsonElement("fulfillmentCounts")]
        public Dictionary<string, int> FulfillmentCounts { get; set; }
    }

    public class GiftHistory
    {
        [BsonElement("num_sent")]
        public int NumSent { get; set; }

        [BsonElement("sentTo")]
        public Dictionary<string, DateTime> SentTo { get; set; }

        [BsonElement("num_received")]
        public int NumReceived { get; set; }

        [BsonElement("receivedFrom")]
        public Dictionary<string, DateTime> ReceivedFrom { get; set; }

        [BsonElement("gifts")]
        public object[] Gifts { get; set; }
    }

    public class PromotionStatus
    {
        [BsonElement("promoName")]
        public string PromoName { get; set; }

        [BsonElement("eligible")]
        public bool Eligible { get; set; }

        [BsonElement("redeemed")]
        public bool Redeemed { get; set; }

        [BsonElement("notified")]
        public bool Notified { get; set; }
    }

    public class AllSurveysMetadata
    {
        [BsonElement("numTimesCompleted")]
        public int NumTimesCompleted { get; set; }

        [BsonElement("lastTimeCompleted")]
        public DateTime LastTimeCompleted { get; set; }
    }

    public class SurveyData
    {
        [BsonElement("allSurveysMetadata")]
        public object AllSurveysMetadata { get; set; }

        [BsonElement("metadata")]
        public Dictionary<string, object> Metadata { get; set; }
    }

    public class BanStatus
    {
        [BsonElement("bRequiresUserAck")]
        public bool RequiresUserAck { get; set; }

        [BsonElement("banReasons")]
        public List<string> BanReasons { get; set; }

        [BsonElement("bBanHasStarted")]
        public bool BanHasStarted { get; set; }

        [BsonElement("banStartTimeUtc")]
        public DateTime BanStartTime { get; set; }

        [BsonElement("banDurationDays")]
        public double BanDurationDays { get; set; }

        [BsonElement("exploitProgramName")]
        public string ExploitProgramName { get; set; }

        [BsonElement("additionalInfo")]
        public string AdditionalInfo { get; set; }

        [BsonElement("competitiveBanReason")]
        public string CompetitiveBanReason { get; set; }
    }

    public class UndoCooldown
    {
        [BsonElement("offerId")]
        public string OfferId { get; set; }

        [BsonElement("cooldownExpires")]
        public DateTime CooldownExpires { get; set; }
    }

    public class BanHistory
    {
        [BsonElement("banCount")]
        public Dictionary<string, int> BanCount { get; set; }

        [BsonElement("banTier")]
        public object BanTier { get; set; }
    }

    public class MtxPurchase
    {
        [BsonElement("purchaseId")]
        public string PurchaseId { get; set; }

        [BsonElement("offerId")]
        public string OfferId { get; set; }

        [BsonElement("purchaseDate")]
        public DateTime PurchaseDate { get; set; }

        [BsonElement("freeRefundEligible")]
        public bool FreeRefundEligible { get; set; }

        [BsonElement("fulfillments")]
        public List<object> Fulfillments { get; set; }

        [BsonElement("lootResult")]
        public List<LootResult> LootResult { get; set; }

        [BsonElement("totalMtxPaid")]
        public int TotalMtxPaid { get; set; }

        [BsonElement("metadata")]
        public object Metadata { get; set; }

        [BsonElement("gameContext")]
        public string GameContext { get; set; }
    }

    public class LootResult
    {
        [BsonElement("itemType")]
        public string ItemType { get; set; }

        [BsonElement("itemGuid")]
        public string ItemGuid { get; set; }

        [BsonElement("itemProfile")]
        public string ItemProfile { get; set; }

        [BsonElement("quantity")]
        public int Quantity { get; set; }
    }
}
