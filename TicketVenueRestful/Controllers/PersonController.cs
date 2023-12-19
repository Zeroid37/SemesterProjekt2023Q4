using Microsoft.AspNetCore.Mvc;
using TicketVenueSystem.Business;
using TicketVenueSystem.Model;

namespace TicketVenueRestful.Controllers
{
    public class PersonController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PersonController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Get a list of all eventOrganizers
        /// </summary>
        /// <returns>List of EventOrganizer Objects</returns>
        [HttpGet]
        public ActionResult<List<EventOrganizer>> EventOrganizers()
        {
            EventOrganizerLogic eLogic = new EventOrganizerLogic(_configuration);
            ActionResult<List<EventOrganizer>> foundReturn;
            List<EventOrganizer> eList = eLogic.getAllEventOrganizers();

            foundReturn = Ok(eList);

            return foundReturn;
        }

        /// <summary>
        /// Get a single event organizer by their organizerId
        /// </summary>
        /// <param name="id"></param>
        /// <returns>EventOrganizer object</returns>
        [HttpGet]
        public ActionResult<EventOrganizer> EventOrganizer(String id)
        {
            EventOrganizerLogic eLogic = new EventOrganizerLogic(_configuration);
            ActionResult<EventOrganizer> foundReturn;
            EventOrganizer eOrg = eLogic.getEventOrganizerById(id);
            foundReturn = Ok(eOrg);

            return foundReturn;
        }
    }
}
