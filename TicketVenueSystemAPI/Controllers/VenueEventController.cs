using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TicketVenueSystemAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VenueEventController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public VenueEventController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        //public ActionResult Index () 
        //{ 
            
        //}

    }
}
