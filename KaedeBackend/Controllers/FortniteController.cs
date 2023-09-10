using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace KaedeBackend.Controllers
{
    [ApiController]
    [Route("fortnite/api")]
    public class FortniteController : Controller
    {
        [HttpPost]
        [Route("game/v2/tryPlayOnPlatform/account/{holder}")]
        public ActionResult<string> PlatFormAccount()
        {
            Response.Headers.Add("Content-Type", "text/plain");
            return Content("true");
        }

        [HttpPost]
        [Route("game/v2/grant_access/{holder}")]
        public ActionResult<string> GrantAccess()
        {
            StatusCode(204);
            return Content("{}");
        }

        [HttpGet]
        [Route("receipts/v1/account/{accountId}/receipts")]
        public ActionResult<string> Receipts()
        {
            StatusCode(204);
            return Content("{}");
        }

        [HttpGet]
        [Route("v2/versioncheck/Windows")]
        public ActionResult<object> Versioncheck()
        {
            StatusCode(204);
            return JObject.FromObject(new
            {
                type = "NO_UPDATE"
            });
        }

        [HttpGet]
        [Route("game/v2/enabled_features")]
        public ActionResult<List<string>> EnabledFeatures() => new List<string>();
    }
}
