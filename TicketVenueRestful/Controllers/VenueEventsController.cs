using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketVenueSystem.Business;
using TicketVenueSystem.DB;
using TicketVenueSystem.Model;

namespace TicketVenueSystemAPI.Controllers
{
    public class VenueEventsController : Controller
    {
        private readonly IConfiguration _configuration;

        public VenueEventsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ActionResult Index () 
        {
            VenueEventLogic veLogic = new VenueEventLogic(_configuration);
            List<VenueEvent> venues = veLogic.getAllVenueEvents();

            return View(venues);
        }

        /// <summary>
        /// Create a venueevent from a deserialized VenueEvent object
        /// </summary>
        /// <param name="venueEvent"></param>
        /// <returns>Actionresult</returns>
        [HttpPost]
        public ActionResult createVenueEvent([FromBody]VenueEvent venueEvent)
        {
            ActionResult foundReturn;
            VenueEventLogic veLogic = new VenueEventLogic(_configuration);

            Boolean ok = veLogic.createVenueEvent(venueEvent.venueEvent_ID, venueEvent.price, venueEvent.eventName, venueEvent.startDate, venueEvent.endDate, venueEvent.hall.hallNumber, venueEvent.eventOrganizer.organizerId);
            if (ok)
            {
                foundReturn = Ok();
            }
            else
            {
                foundReturn = new StatusCodeResult(500);    // Internal server error
            }

            return foundReturn;
        }


        [Authorize]
        [HttpGet]
        public ActionResult BookTicket(int id)
        {
            VenueEventLogic veLogic = new VenueEventLogic(_configuration);
            VenueEvent ve = veLogic.getVenueEventById(id);

            return View(ve);
        }

        /// <summary>
        /// Method for booking a ticket
        /// </summary>
        /// <param name="id"></param>
        /// <param name="seat"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>Actionresult</returns>
        [Authorize]
        [HttpPost]
        public ActionResult BookTicket(int id, [FromForm] String seat, [FromForm] DateTime startDate, [FromForm] DateTime endDate) 
        {
            String email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            TicketLogic ticketLogic = new TicketLogic(_configuration);
            Ticket ticket = ticketLogic.createTicketFromForm(id, seat, email, startDate, endDate);
            List<Ticket> tickets = ticketLogic.getAllTicketsBySeatNo(seat);

            if (ticketLogic.validateTicket(tickets, ticket))
            {
                int ticketCount = ticketLogic.getTicketCount();
                ticketLogic.addTicketToDb(ticket, ticketCount);
            }
            else
            {
                
            }
            return RedirectToAction("Index");
        }
    }
}
