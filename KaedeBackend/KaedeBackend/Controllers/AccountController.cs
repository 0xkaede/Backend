using Jose;
using KaedeBackend.Exceptions;
using KaedeBackend.Exceptions.Common;
using KaedeBackend.Models.Profiles;
using KaedeBackend.Models.Responses;
using KaedeBackend.Services;
using KaedeBackend.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static KaedeBackend.Utils.Globals;

namespace KaedeBackend.Controllers
{
    [ApiController]
    [Route("account/api/public/account")]
    public class AccountController : Controller
    {
        private readonly IMongoService _mongoService;

        public AccountController(IMongoService mongoService)
        {
            _mongoService = mongoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<AccountPublicResponse>>> AccountReturn([FromQuery] string accountId)
        {
            TokenVerify.Verify(HttpContext);

            var data = await _mongoService.GetUserProfileById(accountId);

            return new List<AccountPublicResponse>()
            {
                new AccountPublicResponse
                {
                    DisplayName = data.Username,
                    Id = accountId
                }
            };
        }

        [HttpGet]
        [Route("{accountId}")]
        public async Task<ActionResult<AccountInfoResponse>> AccountInfoData(string accountId)
        {
            TokenVerify.Verify(HttpContext);

            var accountInfo = await _mongoService.GetUserProfileById(accountId);
            return new AccountInfoResponse
            {
                Id = accountInfo.AccountId,
                DisplayName = accountInfo.Username,
                Name = "kaede",
                Email = $"[hidden]{accountInfo.Email.Split("@")[1]}",
                FailedLoginAttempts = 0,
                LastLogin = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.sssZ"),
                NumberOfDisplayNameChanges = 0,
                AgeGroup = "UNKNOWN",
                Headless = false,
                Country = "US",
                LastName = "Server",
                PreferredLanguage = "en",
                CanUpdateDisplayName = false,
                TfaEnabled = false,
                EmailVerified = true,
                MinorVerified = false,
                MinorStatus = "NOT_MINOR",
                CabinedMode = false,
                HasHashedEmail = false
            };
        }

        [HttpGet]
        [Route("{accountId}/externalAuths")]
        public ActionResult<object> ExternalAuths(string accountId)
        {
            return new object();
        }

        [HttpGet]
        [Route("displayName/{displayName}")]
        public async Task<ActionResult<AccountPublicResponse>> GetUserDisplayName(string displayName)
        {
            var data = await _mongoService.GetUserProfileById(displayName);

            return new AccountPublicResponse
            {
                DisplayName = data.Username,
                Id = data.AccountId,
                ExternalAuths = new List<string>(),
            };
        }
    }
}
