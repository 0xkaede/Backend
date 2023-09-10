using Jose;
using KaedeBackend.Exceptions;
using KaedeBackend.Exceptions.Common;
using KaedeBackend.Models.Profiles;
using KaedeBackend.Models.Profiles.Attributes;
using KaedeBackend.Models.Profiles.Other;
using KaedeBackend.Models.Requests;
using KaedeBackend.Models.Responses;
using KaedeBackend.Services;
using KaedeBackend.Utils;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        }

        [HttpPost]
        [Route("{accountId}/client/ClientQuestLogin")]
        [Route("{accountId}/client/QueryProfile")]
        [Route("{accountId}/client/SetMtxPlatform")]
        [Route("{accountId}/client/BulkEquipBattleRoyaleCustomization")]
        public async Task<ActionResult<ProfileResponse>> QueryProfile([FromQuery] string profileId, [FromQuery] int rvn, string accountId)
        {
            //TokenVerify.Verify(HttpContext);

            var ApplyProfileChanges = new List<object>();
            var BaseRevision = 0;

            var profiles = await _mongoService.GetFortniteProfileById(accountId);

            switch (profileId)
            {
                case "athena":
                    {
                        var athenaProfile = profiles.AthenaProfile;
                        BaseRevision = athenaProfile.Revision;
                        ApplyProfileChanges.Add(JObject.FromObject(new
                        {
                            profile = athenaProfile,
                            changeType = "fullProfileUpdate"
                        }));
                        break;
                    }
                case "common_core":
                    {
                        var commonCoreProfile = profiles.CommonCoreProfile;
                        BaseRevision = commonCoreProfile.Revision;
                        ApplyProfileChanges.Add(JObject.FromObject(new
                        {
                            profile = commonCoreProfile,
                            changeType = "fullProfileUpdate"
                        }));
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
                        BaseRevision = common_PublicProfile.Revision;
                        ApplyProfileChanges.Add(JObject.FromObject(new
                        {
                            profile = common_PublicProfile,
                            changeType = "fullProfileUpdate"
                        }));
                        break;
                    }
            }

            var reponse = new ProfileResponse
            {
                ProfileRevision = BaseRevision,
                ProfileId = profileId,
                ProfileprofileChangesBaseRevisionRevision = BaseRevision,
                ProfileChanges = ApplyProfileChanges,
                ProfileCommandRevision = BaseRevision,
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
            //TokenVerify.Verify(HttpContext);

            var Profiles = await _mongoService.GetFortniteProfileById(accountId);

            if (Profiles is null)
                throw new BaseException("errors.com.epicgames.modules.profiles.operation_forbidden", $"Unable to find template configuration for profile {profileId}", 10282, "");

            if (profileId != "athena")
                throw new BaseException("errors.com.epicgames.modules.profiles.invalid_command", $"EquipBattleRoyaleCustomization is not valid on {profileId} profile", 10282, "");

            var ProfileAthena = Profiles.AthenaProfile;

            var memory = GetSeasonData(HttpContext);

            if (profileId == "athena") ProfileAthena.Stats.Attributes.SeasonNum = memory.Season;

            var ApplyProfileChanges = new List<object>();
            var BaseRevision = ProfileAthena.Revision;
            var ProfileRevisionCheck = (memory.Build >= 12.20) ? ProfileAthena.CommandRevision : ProfileAthena.Revision;
            var QueryRevision = rvn;

            switch (body.SlotName)
            {
                case "Character":
                    ProfileAthena.Stats.Attributes.FavoriteCharacter = body.ItemToSlot;
                    break;
                case "Backpack":
                    ProfileAthena.Stats.Attributes.FavoriteBackpack = body.ItemToSlot;
                    break;
                case "Pickaxe":
                    ProfileAthena.Stats.Attributes.FavoritePickaxe = body.ItemToSlot;
                    break;
                case "SkyDiveContrail":
                    ProfileAthena.Stats.Attributes.FavoriteSkyDiveContrail = body.ItemToSlot;
                    break;
                case "Glider":
                    ProfileAthena.Stats.Attributes.FavoriteGlider = body.ItemToSlot;
                    break;
                case "MusicPack":
                    ProfileAthena.Stats.Attributes.FavoriteMusicPack = body.ItemToSlot;
                    break;
                case "LoadingScreen":
                    ProfileAthena.Stats.Attributes.FavoriteLoadingScreen = body.ItemToSlot;
                    break;
                case "Dance":
                    ProfileAthena.Stats.Attributes.FavoriteDance[body.IndexWithinSlot] = body.ItemToSlot;
                    break;
                case "ItemWrap":
                    {
                        if (body.IndexWithinSlot >= 0 && body.IndexWithinSlot <= 7)
                            ProfileAthena.Stats.Attributes.FavoriteItemWraps[body.IndexWithinSlot] = body.ItemToSlot;
                        else if (body.IndexWithinSlot == -1)
                            for (int i = 0; i < 7; i++)
                                ProfileAthena.Stats.Attributes.FavoriteItemWraps[i] = body.ItemToSlot;
                        break;
                    }
                default:
                    throw new BaseException("epic.games.slotname.not.found", $"The slot name ({body.SlotName}) cant be found!", 10893, "");
            }

            ApplyProfileChanges.Add(JObject.FromObject(new
            {
                changeType = "statModified",
                name = $"favorite_{body.SlotName.ToLower()}",
                value = body.ItemToSlot
            }));

            if (ApplyProfileChanges.Count > 0)
            {
                ProfileAthena.Revision += 1;
                ProfileAthena.CommandRevision += 1;
                ProfileAthena.Updated = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.sssZ");


                var filter = Builders<ProfilesMongo>.Filter.Eq("accountId", accountId);
                var update = Builders<ProfilesMongo>.Update.Set("athena", ProfileAthena);
                await _mongoService.UpdateFortniteProfile(filter, update);
            }

            return new ProfileResponse
            {
                ProfileRevision = ProfileAthena.Revision,
                ProfileId = profileId,
                ProfileCommandRevision = BaseRevision,
                ProfileChanges = ApplyProfileChanges,
                ProfileprofileChangesBaseRevisionRevision = ProfileAthena.CommandRevision,
                ServerTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.sssZ"),
                ResponseVersion = 1
            };
        }

        [HttpPost]
        [Route("{accountId}/client/MarkItemSeen")]
        public async Task<ActionResult<ProfileResponse>> MarkItemSeen([FromQuery] string profileId, [FromQuery] int rvn,
            [FromBody] MarkItemSeenRequest body, string accountId)
        {
            var Profiles = await _mongoService.GetFortniteProfileById(accountId);

            if (Profiles is null)
                throw new BaseException("errors.com.epicgames.modules.profiles.operation_forbidden", $"Unable to find template configuration for profile {profileId}", 10282, "");

            if (profileId != "athena")
                throw new BaseException("errors.com.epicgames.modules.profiles.invalid_command", $"EquipBattleRoyaleCustomization is not valid on {profileId} profile", 10282, "");

            var ProfileAthena = Profiles.AthenaProfile;

            var memory = GetSeasonData(HttpContext);

            if (profileId == "athena") ProfileAthena.Stats.Attributes.SeasonNum = memory.Season;

            var ApplyProfileChanges = new List<object>();
            var BaseRevision = ProfileAthena.Revision;

            for (int i = 0; i < body.ItemIds.Count(); i++)
            {
                var item = body.ItemIds[i];
                var itemchanged = JObject.FromObject(ProfileAthena.Items.FirstOrDefault(x => x.Key == item).Value.Attributes).ToObject<ItemAttributes>();
                itemchanged.ItemSeen = true;
                ProfileAthena.Items.FirstOrDefault(x => x.Key == item).Value.Attributes = itemchanged;

                ApplyProfileChanges.Add(JObject.FromObject(new
                {
                    changeType = "itemAttrChanged",
                    itemId = item,
                    attributeName = "item_seen"
                }));
            }

            if (ApplyProfileChanges.Count > 0)
            {
                ProfileAthena.Revision += 1;
                ProfileAthena.CommandRevision += 1;
                ProfileAthena.Updated = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.sssZ");

                var filter = Builders<ProfilesMongo>.Filter.Eq("accountId", accountId);
                var update = Builders<ProfilesMongo>.Update.Set("athena", ProfileAthena);
                await _mongoService.UpdateFortniteProfile(filter, update);
            }

            ApplyProfileChanges = new List<object>();

            ApplyProfileChanges.Add(JObject.FromObject(new
            {
                changeType = "fullProfileUpdate",
                profile = ProfileAthena,
            }));

            return new ProfileResponse
            {
                ProfileRevision = ProfileAthena.Revision,
                ProfileId = profileId,
                ProfileCommandRevision = BaseRevision,
                ProfileChanges = ApplyProfileChanges,
                ProfileprofileChangesBaseRevisionRevision = ProfileAthena.CommandRevision,
                ServerTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.sssZ"),
                ResponseVersion = 1
            };
        }

    }
}
