using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketVenueSystem.Model;
using TicketVenueSystem.DB;
using System.Net.WebSockets;

namespace TicketVenueSystemAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TestController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet, Route("test")]
        public String get()
        {
            VenueEventDB vedb = new VenueEventDB(_configuration);
            List<VenueEvent> venues = vedb.getAllVenueEvents();

            foreach(VenueEvent v in venues)
            {
                Console.WriteLine(v.eventName);
            }


            return "Niggaaa";
        }
    }
}
