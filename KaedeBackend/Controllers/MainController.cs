using Microsoft.AspNetCore.Mvc;

namespace KaedeBackend.Controllers
{
    [ApiController]
    public class MainController : Controller
    {
        [HttpPost]
        [Route("datarouter/api/v1/public/data")]
        public ActionResult Datarouter()
        {
            StatusCode(204);
            return Content("");
        }
    }
}
