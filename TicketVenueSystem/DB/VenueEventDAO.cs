using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketVenueSystem.DB
{
    internal interface VenueEventDAO
    {
        public VenueEvent getVenueEventById(SqlConnection con, String id);
        public bool addVenueEventToDB(SqlConnection con, String id);
    }
}
