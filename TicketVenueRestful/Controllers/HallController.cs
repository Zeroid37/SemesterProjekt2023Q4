using Microsoft.AspNetCore.Mvc;
using TicketVenueSystem.Business;
using TicketVenueSystem.Model;

namespace TicketVenueRestful.Controllers
{
    public class HallController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public HallController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult<Hall> Halls(String id)
        {
            Console.WriteLine(id);
            HallLogic hLogic = new HallLogic(_configuration);
            ActionResult<Hall> foundReturn;

            Hall hall = hLogic.getHallFromHallNo(id.ToString());

            foundReturn = Ok(hall);

            return foundReturn;
        }
    }
}
