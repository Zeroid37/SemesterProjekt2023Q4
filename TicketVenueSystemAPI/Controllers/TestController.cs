using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketVenueSystem.Model;
using TicketVenueSystem.DB;
using System.Net.WebSockets;
using System.Configuration;

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
            UserDB udb = new UserDB(_configuration);

            User u = udb.getUserByEmail("Kasper@mail");

            Console.WriteLine(u.firstName + " " + u.lastName + " " + u.address.city + " " + u.userId);
            return "Niggaaa";
        }
    }
}
