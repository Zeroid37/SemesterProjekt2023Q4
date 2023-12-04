using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public ActionResult BookTicket(int id)
        {
            VenueEventLogic veLogic = new VenueEventLogic(_configuration);
            VenueEvent ve = veLogic.getVenueEventById(id);

            Ticket ticket = new Ticket();
            ticket.venueEvent = ve;

            return View(ticket);
        }

        [HttpPost]
        public ActionResult BookTicket(int id, [FromForm] String seat, [FromForm] String user, [FromForm] DateTime startDate, [FromForm] DateTime endDate) 
        {
            TicketLogic ticketLogic = new TicketLogic(_configuration);
            Ticket ticket = ticketLogic.createTicketFromForm(id, seat, user, startDate, endDate);
            List<Ticket> tickets = ticketLogic.getAllTicketsBySeatNo(seat);
            
            if(ticketLogic.validateTicket(tickets, ticket)) {
                ticketLogic.addTicketToDb(ticket);
                Console.WriteLine("Controller True");
            }
            else
            {
                Console.WriteLine("Controller False");
            }

            return RedirectToAction("Index");
        }

    }
}
