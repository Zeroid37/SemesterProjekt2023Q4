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
            VenueEventDB vedb = new VenueEventDB(_configuration);
            List<VenueEvent> venues = vedb.getAllVenueEvents();

            return View(venues);
        }

        [HttpGet]
        public ActionResult BookTicket(int id)
        {
            VenueEventDB vedb = new VenueEventDB(_configuration);
            VenueEvent ve = vedb.getVenueEventById(id);
            Ticket ticket = new Ticket();
            ticket.venueEvent = ve;

            return View(ticket);
        }

        [HttpPost]
        public ActionResult BookTicket(int id, [FromForm] String seat, [FromForm] String user, [FromForm] DateTime startDate, [FromForm] DateTime endDate) 
        {
            //DELETE THIS
            Random rnd = new Random();
            int num = rnd.Next();

            VenueEventDB vedb = new VenueEventDB(_configuration);
            SeatDB sdb = new SeatDB(_configuration);
            UserDB udb = new UserDB(_configuration);
            TicketDB tdb = new TicketDB(_configuration);
            TicketLogic ticketLogic = new TicketLogic();

            User u = udb.getUserByEmail(user);
            Seat s = sdb.getSeatFromSeatNo(seat);
            VenueEvent ve = vedb.getVenueEventById(id);
            
            List<Ticket> tickets = tdb.getAllTicketsBySeatNo(seat);


            Ticket ticket = new Ticket();
            ticket.venueEvent = ve;
            ticket.startDate = startDate;
            Console.WriteLine(startDate);
            
            ticket.endDate = endDate;
            Console.WriteLine(endDate);
            ticket.user = u;
            ticket.seat = s;
            ticket.ticket_ID = num.ToString();

            
            
            if(ticketLogic.validateTicket(tickets, ticket))
            {
                Console.WriteLine("Controller True");
                tdb.addTicketToDB(ticket);
            }
            else
            {
                Console.WriteLine("Controller False");
            }

            return RedirectToAction("Index");
        }

    }
}
