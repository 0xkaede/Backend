using KaedeBackend.Exceptions;
using KaedeBackend.Exceptions.Common;
using KaedeBackend.Models.Other;
using KaedeBackend.Models.Profiles;
using KaedeBackend.Models.Profiles.Attributes;
using KaedeBackend.Models.Profiles.Other;
using KaedeBackend.Utils;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.VisualBasic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Runtime.CompilerServices;
using static KaedeBackend.Utils.Globals;

namespace KaedeBackend.Services
{
    public interface IMongoService
    {
        public void Ping();

        public Task InitDatabase();

        public Task<UserProfile> LoginAccount(string email, string password);

        public Task<List<UserProfile>> GetAllUserProfiles();

        public Task<UserProfile> GetUserProfile(FilterDefinition<UserProfile> filter);
        public Task<ProfilesMongo> GetFortniteProfile(FilterDefinition<ProfilesMongo> filter);

        public Task<UserProfile> GetUserProfileById(string accountId);
        public Task<ProfilesMongo> GetFortniteProfileById(string accountId);

        public Task UpdateFortniteProfile(FilterDefinition<ProfilesMongo> filter, UpdateDefinition<ProfilesMongo> update);
        public Task UpdateAthenaProfile(string accountId, AthenaProfile profile);
    }

    public class MongoService : IMongoService
    {
        private readonly IMongoCollection<UserProfile> _userProfiles;
        private readonly IMongoCollection<ProfilesMongo> _fortniteProfiles;

        public MongoService()
        {
            var client = new MongoClient("mongodb://localhost");

            var mongoDatabase = client.GetDatabase("KaedeBackend");

            _userProfiles = mongoDatabase.GetCollection<UserProfile>("UserProfiles");
            _fortniteProfiles = mongoDatabase.GetCollection<ProfilesMongo>("FortniteProfiles");

            _ = InitDatabase();
        }

        public void Ping()
        {
        }

        public async Task InitDatabase()
        {
            Logger.Log("Database Is Online");
            CreateAccount("kaede", "kaede@fn.dev", "kaede1234", "1938918931");
            //FullLocker("cb016e60448c4e9fbe3276a05bfdafcf");
        }

        public async Task<List<UserProfile>> GetAllUserProfiles()
        {
            var item = await _userProfiles.FindAsync(x => true);
            return item.ToList();
        }

        public async Task<UserProfile> GetUserProfile(FilterDefinition<UserProfile> filter)
        {
            var items = await _userProfiles.FindAsync(filter);
            return await items.FirstOrDefaultAsync();
        }

        public async Task<ProfilesMongo> GetFortniteProfile(FilterDefinition<ProfilesMongo> filter)
        {
            var items = await _fortniteProfiles.FindAsync(filter);
            return await items.FirstOrDefaultAsync();
        }

        public async Task<UserProfile> GetUserProfileById(string accountId)
        {
            var filter = Builders<UserProfile>.Filter.Eq("accountId", accountId);
            return await GetUserProfile(filter);
        }

        public async Task<ProfilesMongo> GetFortniteProfileById(string accountId)
        {
            var filter = Builders<ProfilesMongo>.Filter.Eq("accountId", accountId);
            return await GetFortniteProfile(filter);
        }

        public async Task UpdateFortniteProfile(FilterDefinition<ProfilesMongo> filter, UpdateDefinition<ProfilesMongo> update)
            => await _fortniteProfiles.UpdateOneAsync(filter, update);

        public async Task UpdateAthenaProfile(string accountId, AthenaProfile profile)
        {
            var filter = Builders<ProfilesMongo>.Filter.Eq("accountId", accountId);
            var update = Builders<ProfilesMongo>.Update.Set("athena", profile);
            await UpdateFortniteProfile(filter, update);
        }
            

        public async Task UpdateUser(FilterDefinition<UserProfile> filter, UpdateDefinition<UserProfile> update)
            => await _userProfiles.UpdateOneAsync(filter, update);

        public async Task CreateAccount(string username, string email, string password, string discordId)
        {
            try
            {
                var users = await GetAllUserProfiles();

                var usernameCheck = users.FirstOrDefault(x => x.Username == username);
                if (usernameCheck != null)
                    throw new UsernameTakenException();

                var emailChekc = users.FirstOrDefault(x => x.Email == email);
                if (emailChekc != null)
                    throw new BaseException("", "Email has already been taken", 108, "");

                var id = CreateUuid();
                var time = CurrentTime();

                var userProfile = new UserProfile
                {
                    AccountId = id,
                    Created = time,
                    discordId = discordId,
                    Email = email,
                    Password = password.ComputeSHA256Hash(),
                    Username = username,
                    banned = false,
                    VBucks = 100000
                };
                #region Athena

                var athenaProfile = new AthenaProfile
                {
                    AccountId = id,
                    Created = time,
                    Updated = time,
                    CommandRevision = 0,
                    Revision = 0,
                    ProfileId = "athena",
                    Items = new Dictionary<string, ProfileItem>(),
                    Stats = new AthenaStats
                    {
                        Attributes = new AthenaAtrributes
                        {
                            FavoriteCharacter = "",
                            FavoriteBackpack = "",
                            FavoriteDance = new List<string> { "", "", "", "", "" },
                            FavoriteGlider = "",
                            FavoriteItemWraps = new List<string> { "", "", "", "", "", ""},
                            FavoriteLoadingScreen = "",
                            FavoriteMusicPack = "",
                            FavoritePickaxe = "",
                            FavoriteSkyDiveContrail = "",
                            SeasonMatchBoost = 0,
                            SeasonFriendMatchBoost = 0,
                            SeasonNum = 0,
                            SeasonUpdate = 0,
                            BookLevel = 1,
                            BookPurchased = false,
                            BookXp = 1,
                            BannerColor = "",
                            BannerIcon = "",
                            Xp = 0,
                            Level = 1,
                            AccountLevel = 1,
                            BattleStars = 0,
                        }
                    },
                    Version = "no_version",
                    WipeNumber = 1
                };

                var athenaItems = new AthenaItem
                {
                    AccountId = id,
                    Items = new Dictionary<string, AthenaItems>(),
                    Lockers = new Dictionary<string, AthenaLockers>(),
                };

                var items = new List<string>
                {
                    "AthenaCharacter:CID_001_Athena_Commando_F_Default", "AthenaCharacter:CID_002_Athena_Commando_F_Default", "AthenaCharacter:CID_003_Athena_Commando_F_Default", "AthenaCharacter:CID_004_Athena_Commando_F_Default", "AthenaCharacter:CID_005_Athena_Commando_M_Default", "AthenaCharacter:CID_006_Athena_Commando_M_Default", "AthenaCharacter:CID_007_Athena_Commando_M_Default", "AthenaCharacter:CID_008_Athena_Commando_M_Default", "AthenaCharacter:CID_556_Athena_Commando_F_RebirthDefaultA", "AthenaCharacter:CID_557_Athena_Commando_F_RebirthDefaultB", "AthenaCharacter:CID_558_Athena_Commando_F_RebirthDefaultC", "AthenaCharacter:CID_559_Athena_Commando_F_RebirthDefaultD", "AthenaCharacter:CID_560_Athena_Commando_M_RebirthDefaultA", "AthenaCharacter:CID_561_Athena_Commando_M_RebirthDefaultB", "AthenaCharacter:CID_562_Athena_Commando_M_RebirthDefaultC", "AthenaCharacter:CID_563_Athena_Commando_M_RebirthDefaultD", "AthenaCharacter:cid_a_272_athena_commando_f_prime", "AthenaPickaxe:DefaultPickaxe", "AthenaGlider:DefaultGlider", "AthenaDance:EID_DanceMoves", "AthenaDance:EID_BoogieDown"
                };

                foreach(var item in items)
                {
                    athenaProfile.Items.Add(item, new ProfileItem
                    {
                        TemplateId = item,
                        Quantity = 1,
                        Attributes = new ItemAttributes
                        {
                            ItemSeen = false,
                            Level = 0,
                            XP = 0,
                            Favorite = false,
                            Variants = new List<Models.Profiles.Attributes.Variant>(),
                            MaxLevelBonus = 0,
                        }
                    });
                }

                athenaProfile.Items.Add("sandbox_loadout", new ProfileItem
                {
                    TemplateId = "CosmeticLocker:cosmeticlocker_athena",
                    Quantity = 1,
                    Attributes = new ItemAttributes
                    {
                        LockerSlotsData = new LockerSlotsData
                        {
                            Slots = new Dictionary<string, Slot>
                            {
                                {
                                    "Character", 
                                    new Slot
                                    {
                                        Items = new List<string> { "" },
                                        ActiveVariants = new List<object> { }
                                    }
                                },
                                {
                                    "Backpack",
                                    new Slot
                                    {
                                        Items = new List<string> { "" },
                                        ActiveVariants = new List<object>{ }
                                    }
                                },
                                {
                                    "Pickaxe",
                                    new Slot
                                    {
                                        Items = new List<string> { "" },
                                        ActiveVariants = new List<object>{ }
                                    }
                                },
                                {
                                    "Dance",
                                    new Slot
                                    {
                                        Items = new List<string> { "", "", "", "", "", ""},
                                        ActiveVariants = new List<object>{ }
                                    }
                                },
                                {
                                    "Glider",
                                    new Slot
                                    {
                                        Items = new List<string> { "" },
                                        ActiveVariants = new List<object>{ }
                                    }
                                },
                                {
                                    "ItemWrap",
                                    new Slot
                                    {
                                        Items = new List<string> { "", "", "", "", "", "", "" },
                                        ActiveVariants = new List<object>{ }
                                    }
                                },
                                {
                                    "LoadingScreen",
                                    new Slot
                                    {
                                        Items = new List<string> { "" },
                                        ActiveVariants = new List<object>{ }
                                    }
                                },
                                {
                                    "MusicPack",
                                    new Slot
                                    {
                                        Items = new List<string> { "" },
                                        ActiveVariants = new List<object>{ }
                                    }
                                },
                                {
                                    "SkyDiveContrail",
                                    new Slot
                                    {
                                        Items = new List<string> { "" },
                                        ActiveVariants = new List<object>{ }
                                    }
                                }
                            }
                        },
                        BannerColorTemplate = "",
                        BannerIconTemplate = "",
                        LockerName = "KAEDE",
                        ItemSeen = false,
                        Favorite = false
                    }
                });

                #endregion

                #region Common_Core

                var commonCoreProfile = new CommonCoreProfile
                {
                    AccountId = id,
                    Created = time,
                    Updated = time,
                    CommandRevision = 0,
                    Revision = 0,
                    ProfileId = "common_core",
                    Version = "mo_version",
                    WipeNumber = 1,
                    Items = new Dictionary<string, CommonProfileItem>(),
                    Stats = new CommonCoreStats
                    {
                        Attributes = new CommonCoreAtrributes
                        {
                            CurrentMtxPlatform = "EpicPC",
                            BanStatus = new BanStatus(),
                            AllowedToSendGifts = true,
                            MtxPurchaseHistory = new MtxPurchaseHistory
                            {
                                RefundCredits = 3,
                                RefundsUsed = 0,
                                Purchases = new List<MtxPurchase>(),
                            },
                            AllowedToReceiveGifts = true,
                        }
                    }
                };

                var commonItems = new List<string>()
                {
                    "HomebaseBannerColor:DefaultColor1", "HomebaseBannerColor:DefaultColor2", "HomebaseBannerColor:DefaultColor3", "HomebaseBannerColor:DefaultColor4", "HomebaseBannerColor:DefaultColor5", "HomebaseBannerColor:DefaultColor6", "HomebaseBannerColor:DefaultColor7", "HomebaseBannerColor:DefaultColor8", "HomebaseBannerColor:DefaultColor9", "HomebaseBannerColor:DefaultColor10", "HomebaseBannerColor:DefaultColor11", "HomebaseBannerColor:DefaultColor12", "HomebaseBannerColor:DefaultColor13", "HomebaseBannerColor:DefaultColor14", "HomebaseBannerColor:DefaultColor15", "HomebaseBannerColor:DefaultColor16", "HomebaseBannerColor:DefaultColor17", "HomebaseBannerColor:DefaultColor18", "HomebaseBannerColor:DefaultColor19", "HomebaseBannerColor:DefaultColor20", "HomebaseBannerColor:DefaultColor21", "HomebaseBannerIcon:StandardBanner1", "HomebaseBannerIcon:StandardBanner2", "HomebaseBannerIcon:StandardBanner3", "HomebaseBannerIcon:StandardBanner4", "HomebaseBannerIcon:StandardBanner5", "HomebaseBannerIcon:StandardBanner6", "HomebaseBannerIcon:StandardBanner7", "HomebaseBannerIcon:StandardBanner8", "HomebaseBannerIcon:StandardBanner9", "HomebaseBannerIcon:StandardBanner10", "HomebaseBannerIcon:StandardBanner11", "HomebaseBannerIcon:StandardBanner12", "HomebaseBannerIcon:StandardBanner13", "HomebaseBannerIcon:StandardBanner14", "HomebaseBannerIcon:StandardBanner15", "HomebaseBannerIcon:StandardBanner16", "HomebaseBannerIcon:StandardBanner17", "HomebaseBannerIcon:StandardBanner18", "HomebaseBannerIcon:StandardBanner19", "HomebaseBannerIcon:StandardBanner20", "HomebaseBannerIcon:StandardBanner21", "HomebaseBannerIcon:StandardBanner22", "HomebaseBannerIcon:StandardBanner23", "HomebaseBannerIcon:StandardBanner24", "HomebaseBannerIcon:StandardBanner25", "HomebaseBannerIcon:StandardBanner26", "HomebaseBannerIcon:StandardBanner27", "HomebaseBannerIcon:StandardBanner28", "HomebaseBannerIcon:StandardBanner29", "HomebaseBannerIcon:StandardBanner30", "HomebaseBannerIcon:StandardBanner31", "HomebaseBannerIcon:FounderTier1Banner1", "HomebaseBannerIcon:FounderTier1Banner2", "HomebaseBannerIcon:FounderTier1Banner3", "HomebaseBannerIcon:FounderTier1Banner4", "HomebaseBannerIcon:FounderTier2Banner1", "HomebaseBannerIcon:FounderTier2Banner2", "HomebaseBannerIcon:FounderTier2Banner3", "HomebaseBannerIcon:FounderTier2Banner4", "HomebaseBannerIcon:FounderTier2Banner5", "HomebaseBannerIcon:FounderTier2Banner6", "HomebaseBannerIcon:FounderTier3Banner1", "HomebaseBannerIcon:FounderTier3Banner2", "HomebaseBannerIcon:FounderTier3Banner3", "HomebaseBannerIcon:FounderTier3Banner4", "HomebaseBannerIcon:FounderTier3Banner5", "HomebaseBannerIcon:FounderTier4Banner1", "HomebaseBannerIcon:FounderTier4Banner2", "HomebaseBannerIcon:FounderTier4Banner3", "HomebaseBannerIcon:FounderTier4Banner4", "HomebaseBannerIcon:FounderTier4Banner5", "HomebaseBannerIcon:FounderTier5Banner1", "HomebaseBannerIcon:FounderTier5Banner2", "HomebaseBannerIcon:FounderTier5Banner3", "HomebaseBannerIcon:FounderTier5Banner4", "HomebaseBannerIcon:FounderTier5Banner5", "HomebaseBannerIcon:NewsletterBanner", "HomebaseBannerIcon:InfluencerBanner1", "HomebaseBannerIcon:InfluencerBanner2", "HomebaseBannerIcon:InfluencerBanner3", "HomebaseBannerIcon:InfluencerBanner4", "HomebaseBannerIcon:InfluencerBanner5", "HomebaseBannerIcon:InfluencerBanner6", "HomebaseBannerIcon:InfluencerBanner7", "HomebaseBannerIcon:InfluencerBanner8", "HomebaseBannerIcon:InfluencerBanner9", "HomebaseBannerIcon:InfluencerBanner10", "HomebaseBannerIcon:InfluencerBanner11", "HomebaseBannerIcon:InfluencerBanner12", "HomebaseBannerIcon:InfluencerBanner13", "HomebaseBannerIcon:InfluencerBanner14", "HomebaseBannerIcon:InfluencerBanner15", "HomebaseBannerIcon:InfluencerBanner16", "HomebaseBannerIcon:InfluencerBanner17", "HomebaseBannerIcon:InfluencerBanner18", "HomebaseBannerIcon:InfluencerBanner19", "HomebaseBannerIcon:InfluencerBanner20", "HomebaseBannerIcon:InfluencerBanner21", "HomebaseBannerIcon:InfluencerBanner22", "HomebaseBannerIcon:InfluencerBanner23", "HomebaseBannerIcon:InfluencerBanner24", "HomebaseBannerIcon:InfluencerBanner25", "HomebaseBannerIcon:InfluencerBanner26", "HomebaseBannerIcon:InfluencerBanner27", "HomebaseBannerIcon:InfluencerBanner28", "HomebaseBannerIcon:InfluencerBanner29", "HomebaseBannerIcon:InfluencerBanner30", "HomebaseBannerIcon:InfluencerBanner31", "HomebaseBannerIcon:InfluencerBanner32", "HomebaseBannerIcon:InfluencerBanner33", "HomebaseBannerIcon:InfluencerBanner34", "HomebaseBannerIcon:InfluencerBanner35", "HomebaseBannerIcon:InfluencerBanner36", "HomebaseBannerIcon:InfluencerBanner37", "HomebaseBannerIcon:InfluencerBanner38", "HomebaseBannerIcon:InfluencerBanner39", "HomebaseBannerIcon:InfluencerBanner40", "HomebaseBannerIcon:InfluencerBanner41", "HomebaseBannerIcon:InfluencerBanner42", "HomebaseBannerIcon:InfluencerBanner43", "HomebaseBannerIcon:InfluencerBanner44", "HomebaseBannerIcon:InfluencerBanner45", "HomebaseBannerIcon:InfluencerBanner46", "HomebaseBannerIcon:InfluencerBanner47", "HomebaseBannerIcon:InfluencerBanner48", "HomebaseBannerIcon:InfluencerBanner49", "HomebaseBannerIcon:InfluencerBanner50", "HomebaseBannerIcon:InfluencerBanner51", "HomebaseBannerIcon:InfluencerBanner52", "HomebaseBannerIcon:InfluencerBanner53", "HomebaseBannerIcon:OT1Banner", "HomebaseBannerIcon:OT2Banner", "HomebaseBannerIcon:OT3Banner", "HomebaseBannerIcon:OT4Banner", "HomebaseBannerIcon:OT5Banner", "HomebaseBannerIcon:OT6Banner", "HomebaseBannerIcon:OT7Banner", "HomebaseBannerIcon:OT8Banner", "HomebaseBannerIcon:OT9Banner", "HomebaseBannerIcon:OT10Banner", "HomebaseBannerIcon:OT11Banner", "HomebaseBannerIcon:OtherBanner1", "HomebaseBannerIcon:OtherBanner2", "HomebaseBannerIcon:OtherBanner3", "HomebaseBannerIcon:OtherBanner4", "HomebaseBannerIcon:OtherBanner5", "HomebaseBannerIcon:OtherBanner6", "HomebaseBannerIcon:OtherBanner7", "HomebaseBannerIcon:OtherBanner8", "HomebaseBannerIcon:OtherBanner9", "HomebaseBannerIcon:OtherBanner10", "HomebaseBannerIcon:OtherBanner11", "HomebaseBannerIcon:OtherBanner12", "HomebaseBannerIcon:OtherBanner13", "HomebaseBannerIcon:OtherBanner14", "HomebaseBannerIcon:OtherBanner15", "HomebaseBannerIcon:OtherBanner16", "HomebaseBannerIcon:OtherBanner17", "HomebaseBannerIcon:OtherBanner18", "HomebaseBannerIcon:OtherBanner19", "HomebaseBannerIcon:OtherBanner20", "HomebaseBannerIcon:OtherBanner21", "HomebaseBannerIcon:OtherBanner22", "HomebaseBannerIcon:OtherBanner23", "HomebaseBannerIcon:OtherBanner24", "HomebaseBannerIcon:OtherBanner25", "HomebaseBannerIcon:OtherBanner26", "HomebaseBannerIcon:OtherBanner27", "HomebaseBannerIcon:OtherBanner28", "HomebaseBannerIcon:OtherBanner29", "HomebaseBannerIcon:OtherBanner30", "HomebaseBannerIcon:OtherBanner31", "HomebaseBannerIcon:OtherBanner32", "HomebaseBannerIcon:OtherBanner33", "HomebaseBannerIcon:OtherBanner34", "HomebaseBannerIcon:OtherBanner35", "HomebaseBannerIcon:OtherBanner36", "HomebaseBannerIcon:OtherBanner37", "HomebaseBannerIcon:OtherBanner38", "HomebaseBannerIcon:OtherBanner39", "HomebaseBannerIcon:OtherBanner40", "HomebaseBannerIcon:OtherBanner41", "HomebaseBannerIcon:OtherBanner42", "HomebaseBannerIcon:OtherBanner43", "HomebaseBannerIcon:OtherBanner44", "HomebaseBannerIcon:OtherBanner45", "HomebaseBannerIcon:OtherBanner46", "HomebaseBannerIcon:OtherBanner47", "HomebaseBannerIcon:OtherBanner48", "HomebaseBannerIcon:OtherBanner49", "HomebaseBannerIcon:OtherBanner50", "HomebaseBannerIcon:OtherBanner51", "HomebaseBannerIcon:OtherBanner52", "HomebaseBannerIcon:OtherBanner53", "HomebaseBannerIcon:OtherBanner54", "HomebaseBannerIcon:OtherBanner55", "HomebaseBannerIcon:OtherBanner56", "HomebaseBannerIcon:OtherBanner57", "HomebaseBannerIcon:OtherBanner58", "HomebaseBannerIcon:OtherBanner59", "HomebaseBannerIcon:OtherBanner60", "HomebaseBannerIcon:OtherBanner61", "HomebaseBannerIcon:OtherBanner62", "HomebaseBannerIcon:OtherBanner63", "HomebaseBannerIcon:OtherBanner64", "HomebaseBannerIcon:OtherBanner65", "HomebaseBannerIcon:OtherBanner66", "HomebaseBannerIcon:OtherBanner67", "HomebaseBannerIcon:OtherBanner68", "HomebaseBannerIcon:OtherBanner69", "HomebaseBannerIcon:OtherBanner70", "HomebaseBannerIcon:OtherBanner71", "HomebaseBannerIcon:OtherBanner72", "HomebaseBannerIcon:OtherBanner73", "HomebaseBannerIcon:OtherBanner74", "HomebaseBannerIcon:OtherBanner75", "HomebaseBannerIcon:OtherBanner76", "HomebaseBannerIcon:OtherBanner77", "HomebaseBannerIcon:OtherBanner78", "HomebaseBannerIcon:InfluencerBanner54", "HomebaseBannerIcon:InfluencerBanner55", "HomebaseBannerIcon:InfluencerBanner56", "HomebaseBannerIcon:InfluencerBanner57", "HomebaseBannerIcon:InfluencerBanner58", "HomebaseBannerIcon:SurvivalBannerStonewoodComplete", "HomebaseBannerIcon:SurvivalBannerPlankertonComplete", "HomebaseBannerIcon:SurvivalBannerCannyValleyComplete", "HomebaseBannerIcon:BRSeason01", "HomebaseBannerIcon:SurvivalBannerHolidayEasy", "HomebaseBannerIcon:SurvivalBannerHolidayMed", "HomebaseBannerIcon:SurvivalBannerHolidayHard", "HomebaseBannerIcon:BRSeason02Bush", "HomebaseBannerIcon:BRSeason02Salt", "HomebaseBannerIcon:BRSeason02LionHerald", "HomebaseBannerIcon:BRSeason02CatSoldier", "HomebaseBannerIcon:BRSeason02Dragon", "HomebaseBannerIcon:BRSeason02Planet", "HomebaseBannerIcon:BRSeason02Crosshair", "HomebaseBannerIcon:BRSeason02Bowling", "HomebaseBannerIcon:BRSeason02MonsterTruck", "HomebaseBannerIcon:BRSeason02Shark", "HomebaseBannerIcon:BRSeason02IceCream", "HomebaseBannerIcon:BRSeason02Log", "HomebaseBannerIcon:BRSeason02Cake", "HomebaseBannerIcon:BRSeason02Tank", "HomebaseBannerIcon:BRSeason02GasMask", "HomebaseBannerIcon:BRSeason02Vulture", "HomebaseBannerIcon:BRSeason02Sawhorse", "HomebaseBannerIcon:BRSeason03Bee", "HomebaseBannerIcon:BRSeason03Chick", "HomebaseBannerIcon:BRSeason03Chicken", "HomebaseBannerIcon:BRSeason03Crab", "HomebaseBannerIcon:BRSeason03CrescentMoon", "HomebaseBannerIcon:BRSeason03DeadFish", "HomebaseBannerIcon:BRSeason03DinosaurSkull", "HomebaseBannerIcon:BRSeason03DogCollar", "HomebaseBannerIcon:BRSeason03Donut", "HomebaseBannerIcon:BRSeason03Eagle", "HomebaseBannerIcon:BRSeason03Egg", "HomebaseBannerIcon:BRSeason03Falcon", "HomebaseBannerIcon:BRSeason03Flail", "HomebaseBannerIcon:BRSeason03HappyCloud", "HomebaseBannerIcon:BRSeason03Lantern", "HomebaseBannerIcon:BRSeason03Lightning", "HomebaseBannerIcon:BRSeason03Paw", "HomebaseBannerIcon:BRSeason03Shark", "HomebaseBannerIcon:BRSeason03ShootingStar", "HomebaseBannerIcon:BRSeason03Snake", "HomebaseBannerIcon:BRSeason03Sun", "HomebaseBannerIcon:BRSeason03TeddyBear", "HomebaseBannerIcon:BRSeason03TopHat", "HomebaseBannerIcon:BRSeason03Whale", "HomebaseBannerIcon:BRSeason03Wing", "HomebaseBannerIcon:BRSeason03Wolf", "HomebaseBannerIcon:BRSeason03Worm", "HomebaseBannerIcon:SurvivalBannerStorm2018Easy", "HomebaseBannerIcon:SurvivalBannerStorm2018Med", "HomebaseBannerIcon:SurvivalBannerStorm2018Hard"
                };

                foreach (var item in commonItems)
                {
                    commonCoreProfile.Items.Add(item, new CommonProfileItem
                    {
                        TemplateId = item,
                        Quantity = 1,
                        Attributes = new CommonItemAttributes
                        {
                            ItemSeen = true,
                        }
                    });
                }

                commonCoreProfile.Items.Add("Currency:MtxPurchased", new CommonProfileItem
                {
                    TemplateId = "Currency:MtxPurchased",
                    Quantity = 5000,
                    Attributes = new CommonItemAttributes
                    {
                        Platform = "EpicPC"
                    }
                });

                commonCoreProfile.Items.Add("Token:FounderChatUnlock", new CommonProfileItem
                {
                    TemplateId = "Token:FounderChatUnlock",
                    Quantity = 1,
                    Attributes = new CommonItemAttributes
                    {
                        ItemSeen = true,
                        Level = 0,
                        XP = 0,
                        Favorite = false,
                        MaxLevelBonus = 0,
                    }
                });

                #endregion

                var profile = new ProfilesMongo
                {
                    AccountId = id,
                    AthenaProfile = athenaProfile,
                    CommonCoreProfile = commonCoreProfile,
                };

                await _userProfiles.InsertOneAsync(userProfile);
                await _fortniteProfiles.InsertOneAsync(profile);

                Logger.Log($"Account Created: {username}");
            }
            catch(Exception ex)
            {
                Logger.Log(ex.ToString(), Utils.LogLevel.Error);
            }
        }

        public async Task<UserProfile> LoginAccount(string email, string password)
        {
            var data = await GetAllUserProfiles();

            var user = data.FirstOrDefault(x => x.Email == email);

            if (user is null)
                throw new InvalidCredentialsException();

            var hashPassword = password.ComputeSHA256Hash();

            if (hashPassword != user.Password)
                throw new InvalidCredentialsException();

            return user;
        }

        public async Task FullLocker(string id)
        {
            var profiles = await GetFortniteProfileById(id);

            var athena = profiles.AthenaProfile;

            var items = JsonConvert.DeserializeObject<FortniteAPIResponse<List<Cosmetic>>>(await new HttpClient().GetStringAsync("https://fortnite-api.com/v2/cosmetics/br")).Data;

            athena.Items = new Dictionary<string, ProfileItem>();

            foreach (var item in items)
            {
                if (item.Id.ToLower().Contains("random")) continue;

                athena.Items.Add($"{item.Type.BackendValue}:{item.Id}", new ProfileItem
                {
                    TemplateId = $"{item.Type.BackendValue}:{item.Id}",
                    Quantity = 1,
                    Attributes = new ItemAttributes
                    {
                        ItemSeen = true,
                        Favorite = false,
                        Variants = new List<Models.Profiles.Attributes.Variant>()
                    }
                });
            }

            var filter = Builders<ProfilesMongo>.Filter.Eq("accountId", id);
            var update = Builders<ProfilesMongo>.Update.Set("athena", athena);
            await UpdateFortniteProfile(filter, update);
            Logger.Log("Full Locker granted");
        }
    }
}
