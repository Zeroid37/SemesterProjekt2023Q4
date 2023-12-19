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

        public VenueEventLogic(IConfiguration configuration) {
            Configuration = configuration;
        }

        /// <summary>
        /// Get a venue event by it's ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Venue event object</returns>
        public VenueEvent getVenueEventById(int id) {
            VenueEventDAO vedb = new VenueEventDB(Configuration);
            VenueEvent ve = vedb.getVenueEventById(id);
            return ve;
        }

        /// <summary>
        /// Get all venue events in the database
        /// </summary>
        /// <returns>List of venue events</returns>
        public List<VenueEvent> getAllVenueEvents() {
            VenueEventDAO vedb = new VenueEventDB(Configuration);
            List<VenueEvent> venueEvents = vedb.getAllVenueEvents();
            return venueEvents;
        }

        /// <summary>
        /// Creates a venueEvent object from parameters and attempts to add it to the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="price"></param>
        /// <param name="eventName"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="hallNo"></param>
        /// <param name="eventOrgID"></param>
        /// <returns>Boolean true or false</returns>
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
