using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketVenueSystem.Model;

namespace TicketVenueSystem.DB
{
    internal interface TicketDAO
    {
        public Boolean addTicketToDB(SqlConnection con, Ticket ticket);
        public Boolean removeTicketFromDB(SqlConnection con, Ticket ticket);
        public Ticket getTicketByUserID(SqlConnection con, string userID);
    }
}
