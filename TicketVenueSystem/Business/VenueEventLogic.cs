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
    public class VenueEventLogic
    {
        private IConfiguration Configuration;
        private String? connectionString;

        public VenueEventLogic(IConfiguration configuration) {
            Configuration = configuration;
            connectionString = Configuration.GetConnectionString("DefaultConnection");
        }

        public VenueEvent getVenueEventById(int id) {
            VenueEventDAO vedb = new VenueEventDB(Configuration);
            VenueEvent ve = vedb.getVenueEventById(id);
            return ve;
        }

        public List<VenueEvent> getAllVenueEvents() {
            VenueEventDAO vedb = new VenueEventDB(Configuration);
            List<VenueEvent> venueEvents = vedb.getAllVenueEvents();
            return venueEvents;
        }

        public Boolean createVenueEvent(string id, double price, String eventName, DateTime startDate, DateTime endDate, String hallNo, String eventOrgID)
        {
            Boolean res = false;

            UserDAO udb = new UserDB(Configuration);
            HallDAO hdb = new HallDB(Configuration);
            VenueEventDAO vedb = new VenueEventDB(Configuration);
            Hall hall = hdb.getHallFromHallNo(hallNo);
            EventOrganizer evOrg = udb.getEventOrganizerByID(eventOrgID);

            VenueEvent ve = new VenueEvent(id, price, eventName, startDate, endDate, hall, evOrg);

            res = vedb.addVenueEventToDB(ve);

            return res;
        }


    }
}
