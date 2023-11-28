using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketVenueSystem.Model;
using System.Data.SqlClient;

namespace TicketVenueSystem.DB
{
    internal interface VenueEventDAO
    {
        public VenueEvent getVenueEventById(int id);
        public bool addVenueEventToDB(VenueEvent venueEvent);
    }
}
