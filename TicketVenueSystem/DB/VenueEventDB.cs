using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketVenueSystem.Model;

namespace TicketVenueSystem.DB
{
    internal class VenueEventDB : VenueEventDAO
    {
        public bool addVenueEventToDB(SqlConnection con, string id)
        {
            throw new NotImplementedException();
        }

        public VenueEvent getVenueEventById(SqlConnection con, string id)
        {
            throw new NotImplementedException();
        }
    }
}
