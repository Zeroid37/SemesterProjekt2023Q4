using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketVenueSystem.Model;

namespace TicketVenueSystem.DB
{
    internal interface HallDAO
    {
        public Hall getHallFromHallNo(SqlConnection con, int hallNo);
        public List<Hall> getAllHallsFromHallNo(SqlConnection con, int hallNo);
    }
}
