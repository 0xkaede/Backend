using KaedeBackend.Exceptions;
using KaedeBackend.Models.Profiles;
using KaedeBackend.Models.Profiles.Changes;
using KaedeBackend.Models.Requests;
using KaedeBackend.Models.Responses;
using KaedeBackend.Services;
using KaedeBackend.Utils;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using static KaedeBackend.Utils.Globals;

namespace KaedeBackend.Controllers
{
    [ApiController]
    [Route("fortnite/api/game/v2/profile")]
    public class McpController : Controller
    {
        private readonly IMongoService _mongoService;

        public McpController(IMongoService mongoService)
        {
            _mongoService = mongoService;

            TokenVerify.Verify(HttpContext);
        }

        [HttpPost]
        [Route("{accountId}/client/ClientQuestLogin")]
        [Route("{accountId}/client/QueryProfile")]
        [Route("{accountId}/client/SetMtxPlatform")]
        [Route("{accountId}/client/BulkEquipBattleRoyaleCustomization")]
        public async Task<ActionResult<ProfileResponse>> QueryProfile([FromQuery] string profileId, [FromQuery] int rvn, string accountId)
        {
            var applyProfileChanges = new List<object>();
            var baseRevision = 0;
            var memory = GetSeasonData(HttpContext);

            var profiles = await _mongoService.GetFortniteProfileById(accountId);

            switch (profileId)
            {
                case "athena":
                    {
                        var athenaProfile = profiles.AthenaProfile;
                        baseRevision = athenaProfile.Revision;
                        if (profileId == "athena") athenaProfile.Stats.Attributes.SeasonNum = memory.Season;
                        await _mongoService.UpdateAthenaProfile(accountId, athenaProfile);
                        applyProfileChanges.Add(new FullProfileUpdate
                        {
                            ChangeType = "fullProfileUpdate",
                            Profile = athenaProfile
                        });
                        break;
                    }
                case "common_core":
                    {
                        var commonCoreProfile = profiles.CommonCoreProfile;
                        baseRevision = commonCoreProfile.Revision;
                        applyProfileChanges.Add(new FullProfileUpdate
                        {
                            ChangeType = "fullProfileUpdate",
                            Profile = commonCoreProfile
                        });
                        break;
                    }
                case "common_public":
                    {
                        var common_PublicProfile = new CommonCoreProfile
                        {
                            AccountId = accountId,
                            Created = "",
                            Updated = "",
                            Stats = new CommonCoreStats(),
                            Items = new Dictionary<string, CommonProfileItem>(),
                            CommandRevision = 0,
                            ProfileId = "common_public",
                            Version = "no_version",
                            Revision = 0,
                            WipeNumber = 1
                        };
                        baseRevision = common_PublicProfile.Revision;
                        applyProfileChanges.Add(new FullProfileUpdate
                        {
                            ChangeType = "fullProfileUpdate",
                            Profile = common_PublicProfile
                        });
                        break;
                    }
            }

            var reponse = new ProfileResponse
            {
                ProfileRevision = baseRevision,
                ProfileId = profileId,
                ProfileChangesBaseRevisionRevision = baseRevision,
                ProfileChanges = applyProfileChanges,
                ProfileCommandRevision = baseRevision,
                ServerTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.sssZ"),
                ResponseVersion = 1
            };

            return reponse;
        }

        [HttpPost]
        [Route("{accountId}/client/EquipBattleRoyaleCustomization")]
        public async Task<ActionResult<ProfileResponse>> EquipBattleRoyaleCustomization([FromQuery] string profileId, [FromQuery] int rvn,
            [FromBody] EquipBattleRoyaleCustomizationRequest body, string accountId)
        {
            var profiles = await _mongoService.GetFortniteProfileById(accountId);

            if (profiles is null)
                throw new BaseException("errors.com.epicgames.modules.profiles.operation_forbidden", $"Unable to find template configuration for profile {profileId}", 10282, "");

            if (profileId != "athena")
                throw new BaseException("errors.com.epicgames.modules.profiles.invalid_command", $"EquipBattleRoyaleCustomization is not valid on {profileId} profile", 10282, "");

            var profileAthena = profiles.AthenaProfile;

            var memory = GetSeasonData(HttpContext);

            var applyProfileChanges = new List<object>();
            var baseRevision = profileAthena.Revision;
            var profileRevisionCheck = (memory.Build >= 12.20) ? profileAthena.CommandRevision : profileAthena.Revision;

            switch (body.SlotName)
            {
                case "Character":
                    profileAthena.Stats.Attributes.FavoriteCharacter = body.ItemToSlot;
                    AddChanges(profileAthena.Stats.Attributes.FavoriteCharacter);
                    break;
                case "Backpack":
                    profileAthena.Stats.Attributes.FavoriteBackpack = body.ItemToSlot;
                    AddChanges(profileAthena.Stats.Attributes.FavoriteBackpack);
                    break;
                case "Pickaxe":
                    profileAthena.Stats.Attributes.FavoritePickaxe = body.ItemToSlot;
                    AddChanges(profileAthena.Stats.Attributes.FavoritePickaxe);
                    break;
                case "SkyDiveContrail":
                    profileAthena.Stats.Attributes.FavoriteSkyDiveContrail = body.ItemToSlot;
                    AddChanges(profileAthena.Stats.Attributes.FavoriteSkyDiveContrail);
                    break;
                case "Glider":
                    profileAthena.Stats.Attributes.FavoriteGlider = body.ItemToSlot;
                    AddChanges(profileAthena.Stats.Attributes.FavoriteGlider);
                    break;
                case "MusicPack":
                    profileAthena.Stats.Attributes.FavoriteMusicPack = body.ItemToSlot;
                    AddChanges(profileAthena.Stats.Attributes.FavoriteMusicPack);
                    break;
                case "LoadingScreen":
                    profileAthena.Stats.Attributes.FavoriteLoadingScreen = body.ItemToSlot;
                    AddChanges(profileAthena.Stats.Attributes.FavoriteLoadingScreen);
                    break;
                case "Dance":
                    profileAthena.Stats.Attributes.FavoriteDance[body.IndexWithinSlot] = body.ItemToSlot;
                    AddChanges(profileAthena.Stats.Attributes.FavoriteDance);
                    break;
                case "ItemWrap":
                    {
                        if (body.IndexWithinSlot >= 0 && body.IndexWithinSlot <= 7)
                            profileAthena.Stats.Attributes.FavoriteItemWraps[body.IndexWithinSlot] = body.ItemToSlot;
                        else if (body.IndexWithinSlot == -1)
                            for (int i = 0; i < 7; i++)
                                profileAthena.Stats.Attributes.FavoriteItemWraps[i] = body.ItemToSlot;

                        AddChanges(profileAthena.Stats.Attributes.FavoriteItemWraps);
                        break;
                    }
                default:
                    throw new BaseException("epic.games.slotname.not.found", $"The slot name ({body.SlotName}) cant be found!", 10893, "");
            }
            
            void AddChanges(object obj)
            {
                applyProfileChanges.Add(new StatModified
                {
                    ChangeType = "statModified",
                    Name = $"favorite_{body.SlotName.ToLower()}",
                    Value = obj
                });
            }

            if (applyProfileChanges.Count > 0)
            {
                profileAthena.Revision += 1;
                profileAthena.CommandRevision += 1;
                profileAthena.Updated = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.sssZ");

                await _mongoService.UpdateAthenaProfile(accountId, profileAthena);
            }

            if (rvn != profileRevisionCheck)
            {
                applyProfileChanges = new List<object>
                { 
                    new FullProfileUpdate
                    {
                        ChangeType = "fullProfileUpdate",
                        Profile = profileAthena
                    }
                };
            }

            return new ProfileResponse
            {
                ProfileRevision = profileAthena.Revision,
                ProfileId = profileId,
                ProfileCommandRevision = profileAthena.CommandRevision,
                ProfileChanges = applyProfileChanges,
                ProfileChangesBaseRevisionRevision = baseRevision,
                ServerTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.sssZ"),
                ResponseVersion = 1
            };
        }

        [HttpPost]
        [Route("{accountId}/client/MarkItemSeen")]
        public async Task<ActionResult<ProfileResponse>> MarkItemSeen([FromQuery] string profileId, [FromQuery] int rvn,
            [FromBody] MarkItemSeenRequest body, string accountId)
        {
            var profiles = await _mongoService.GetFortniteProfileById(accountId);
            if (profiles is null)
                throw new BaseException("errors.com.epicgames.modules.profiles.operation_forbidden", $"Unable to find template configuration for profile {profileId}", 10282, "");

            if (profileId != "athena")
                throw new BaseException("errors.com.epicgames.modules.profiles.invalid_command", $"EquipBattleRoyaleCustomization is not valid on {profileId} profile", 10282, "");

            var profileAthena = profiles.AthenaProfile;

            var memory = GetSeasonData(HttpContext);

            var applyProfileChanges = new List<object>();
            var BaseRevision = profileAthena.Revision;
            var profileRevisionCheck = (memory.Build >= 12.20) ? profileAthena.CommandRevision : profileAthena.Revision;

            for (int i = 0; i < body.ItemIds.Count(); i++)
            {
                var item = body.ItemIds[i];

                profileAthena.Items.FirstOrDefault(x => x.Key == item).Value.Attributes.ItemSeen = true;

                applyProfileChanges.Add(new ItemAttrChanged
                {
                    ChangeType = "itemAttrChanged",
                    ItemId = item,
                    AttributeName = "item_seen",
                    AttributeValue = true
                });
            }

            if (applyProfileChanges.Count > 0)
            {
                profileAthena.Revision += 1;
                profileAthena.CommandRevision += 1;
                profileAthena.Updated = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.sssZ");

                var filter = Builders<ProfilesMongo>.Filter.Eq("accountId", accountId);
                var update = Builders<ProfilesMongo>.Update.Set("athena", profileAthena);
                await _mongoService.UpdateFortniteProfile(filter, update);
            }

            if (rvn != profileRevisionCheck)
            {
                applyProfileChanges = new List<object>
                {
                    new FullProfileUpdate
                    {
                        ChangeType = "fullProfileUpdate",
                        Profile = profileAthena
                    }
                };
            }

            return new ProfileResponse
            {
                ProfileRevision = profileAthena.Revision,
                ProfileId = profileId,
                ProfileCommandRevision = profileAthena.CommandRevision,
                ProfileChanges = applyProfileChanges,
                ProfileChangesBaseRevisionRevision = BaseRevision,
                ServerTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.sssZ"),
                ResponseVersion = 1
            };
        }

        [HttpPost]
        [Route("{accountId}/client/SetItemFavoriteStatusBatch")]
        public async Task<ActionResult<ProfileResponse>> SetItemFavoriteStatusBatch([FromQuery] string profileId, [FromQuery] int rvn,
            [FromBody] SetItemFavoriteStatusBatchRequest body, string accountId)
        {
            var profiles = await _mongoService.GetFortniteProfileById(accountId);
            if (profiles is null)
                throw new BaseException("errors.com.epicgames.modules.profiles.operation_forbidden", $"Unable to find template configuration for profile {profileId}", 10282, "");

            if (profileId != "athena")
                throw new BaseException("errors.com.epicgames.modules.profiles.invalid_command", $"EquipBattleRoyaleCustomization is not valid on {profileId} profile", 10282, "");

            var profileAthena = profiles.AthenaProfile;

            var memory = GetSeasonData(HttpContext);

            var applyProfileChanges = new List<object>();
            var BaseRevision = profileAthena.Revision;
            var profileRevisionCheck = (memory.Build >= 12.20) ? profileAthena.CommandRevision : profileAthena.Revision;

            for (int i = 0; i < body.ItemIds.Count(); i++)
            {
                var item = body.ItemIds[i];

                profileAthena.Items.FirstOrDefault(x => x.Key == item).Value.Attributes.ItemSeen = body.ItemFavStatus[i];

                applyProfileChanges.Add(new ItemAttrChanged
                {
                    ChangeType = "itemAttrChanged",
                    ItemId = item,
                    AttributeName = "favorite",
                    AttributeValue = body.ItemFavStatus[i]
                });
            }

            if (applyProfileChanges.Count > 0)
            {
                profileAthena.Revision += 1;
                profileAthena.CommandRevision += 1;
                profileAthena.Updated = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.sssZ");

                var filter = Builders<ProfilesMongo>.Filter.Eq("accountId", accountId);
                var update = Builders<ProfilesMongo>.Update.Set("athena", profileAthena);
                await _mongoService.UpdateFortniteProfile(filter, update);
            }

            if (rvn != profileRevisionCheck)
            {
                applyProfileChanges = new List<object>
                {
                    new FullProfileUpdate
                    {
                        ChangeType = "fullProfileUpdate",
                        Profile = profileAthena
                    }
                };
            }

            return new ProfileResponse
            {
                ProfileRevision = profileAthena.Revision,
                ProfileId = profileId,
                ProfileCommandRevision = profileAthena.CommandRevision,
                ProfileChanges = applyProfileChanges,
                ProfileChangesBaseRevisionRevision = BaseRevision,
                ServerTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.sssZ"),
                ResponseVersion = 1
            };
        }
    }
}
