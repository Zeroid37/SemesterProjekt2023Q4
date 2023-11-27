using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketVenueSystem.Model;
using TicketVenueSystem.DB;

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

            Address adress = new Address("Hjesavej", "5", "9000", "Aalborg");

            string id = "69";
            string firstName = "Smajo";
            string lastName = "Omanovic";
            string email = "Smajo@mail";
            string phoneNo = "1234567890";
            DateTime dateOfBirth = new DateTime(2000, 1, 16);
            string password = "password";
            Boolean isAdmin = false;
            //User user = new User(id, firstName, lastName, adress, phoneNo, email, password, isAdmin, dateOfBirth);
            EventOrganizer e = new EventOrganizer(id, firstName, lastName, adress, phoneNo, email, password, isAdmin, dateOfBirth);

            udb.addEventOrganizerToDB(e);


            return "Niggaaa";
        }
    }
}
