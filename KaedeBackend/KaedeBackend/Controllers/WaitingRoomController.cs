using Microsoft.AspNetCore.Mvc;

namespace KaedeBackend.Controllers
{
    [ApiController]
    [Route("/waitingroom/api/waitingroom")]
    public class WaitingRoomController : Controller
    {
        public ActionResult WatingRoom()
        {
            NoContent();
            return StatusCode(204);
        }
    }
}
