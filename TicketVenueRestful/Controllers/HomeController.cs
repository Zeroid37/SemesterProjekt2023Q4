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
            Console.WriteLine("We entered the Nice zone");

            Address address = new Address("Vejej", "5", "2934", "Copenhagen");
            User newUser = new User();
            newUser.email = "smajo4@mail";
            newUser.firstName = "smajo";
            newUser.lastName = "Omanovic";
            newUser.phoneNo = "99999999";
            newUser.address = address;
            newUser.dateOfBirth = DateTime.Now;

            //DELETE THIS
            Random rnd = new Random();
            int num = rnd.Next();
            newUser.userId = num.ToString();

            UserLogic userLogic = new UserLogic(_configuration);

            userLogic.addUserToDB(newUser);
            userLogic.setAspNetIdByEmail(newUser.email, "4fd7f974-d647-4bbb-a687-bec874da1d75");

            return "Nice";
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}