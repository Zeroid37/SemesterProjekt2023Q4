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

        /// <summary>
        /// Get halls by their hallNumber
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ActionResult of Hall</returns>
        [HttpGet]
        public ActionResult<Hall> Halls(String id)
        {
            HallLogic hLogic = new HallLogic(_configuration);
            ActionResult<Hall> foundReturn;

            Hall hall = hLogic.getHallFromHallNo(id.ToString());

            foundReturn = Ok(hall);

            return foundReturn;
        }
    }
}
