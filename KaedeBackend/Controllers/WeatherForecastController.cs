using Microsoft.AspNetCore.Mvc;

namespace KaedeBackend.Controllers
{
    [ApiController]
    [Route("kaede/api/{accountid}")]
    public class WeatherForecastController : ControllerBase
    {
        private string AccountId { get; set; }
        public WeatherForecastController(string accountid)
        {
            Console.WriteLine(accountid);
            AccountId = accountid;
        }

        [HttpGet("yes")]
        public string Get()
        {
            return AccountId;
        }
    }
}