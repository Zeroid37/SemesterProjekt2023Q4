using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TicketVenueRestful.Models;
using TicketVenueSystem.Business;
using TicketVenueSystem.Model;

namespace TicketVenueRestful.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public String Test()
        {
            EventOrganizerLogic eLogic = new EventOrganizerLogic(_configuration);
            List<EventOrganizer> eList = eLogic.getAllEventOrganizers();

            foreach(EventOrganizer e in eList)
            {
                Console.WriteLine(e.organizerId + " " + e.firstName);
            }

            return "Nice";
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}