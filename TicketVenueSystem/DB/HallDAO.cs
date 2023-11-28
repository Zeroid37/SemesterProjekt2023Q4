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
        public Hall getHallFromHallNo(string hallNo);
        public List<Hall> getAllHallsFromHallNo(string hallNo);
    }
}
