using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketVenueSystem.DB;
using TicketVenueSystem.Model;

namespace TicketVenueSystem.Business
{
    public class EventOrganizerLogic
    {
        private IConfiguration Configuration;
        private String? connectionString;

        public EventOrganizerLogic(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionString = Configuration.GetConnectionString("DefaultConnection");
        }


        public List<EventOrganizer> getAllEventOrganizers()
        {
            UserDAO udb = new UserDB(Configuration);
            List<EventOrganizer> eList = udb.getAllEventOrganizers();

            return eList;
        }

    }
}
