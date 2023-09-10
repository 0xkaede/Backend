using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace KaedeBackend.Controllers
{
    [ApiController]
    [Route("/lightswitch/api/service")]
    public class lightswitchController : Controller
    {
        [HttpGet]
        [Route("Fortnite/status")]
        public ActionResult<string> FrotntieStatus()
            => "{\"serviceInstanceId\":\"fortnite\",\"status\":\"UP\",\"message\":\"Fortniteisonline\",\"maintenanceUri\":null,\"overrideCatalogIds\":[\"a7f138b2e51945ffbfdacc1af0541053\"],\"allowedActions\":[],\"banned\":false,\"launcherInfoDTO\":{\"appName\":\"Fortnite\",\"catalogItemId\":\"4fe75bbc5a674f4f9b356b5c90567da5\",\"namespace\":\"fn\"}}";

        [HttpGet]
        [Route("bulk/status")]
        public ActionResult<string> BulkStatus()
            => "[{\"serviceInstanceId\":\"fortnite\",\"status\":\"UP\",\"message\":\"fortniteisup.\",\"maintenanceUri\":null,\"overrideCatalogIds\":[\"a7f138b2e51945ffbfdacc1af0541053\"],\"allowedActions\":[\"PLAY\",\"DOWNLOAD\"],\"banned\":false,\"launcherInfoDTO\":{\"appName\":\"Fortnite\",\"catalogItemId\":\"4fe75bbc5a674f4f9b356b5c90567da5\",\"namespace\":\"fn\"}}]";
    }
}
