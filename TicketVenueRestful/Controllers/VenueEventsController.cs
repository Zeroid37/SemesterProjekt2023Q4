using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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

        [Authorize]
        [HttpGet]
        public ActionResult BookTicket(int id)
        {
            VenueEventLogic veLogic = new VenueEventLogic(_configuration);
            VenueEvent ve = veLogic.getVenueEventById(id);

            return View(ve);
        }

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
