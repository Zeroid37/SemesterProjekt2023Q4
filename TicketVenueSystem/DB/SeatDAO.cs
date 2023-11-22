using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketVenueSystem.Model;

namespace TicketVenueSystem.DB
{
    internal interface SeatDAO
    {
        public Seat getSeatFromSeatNo(SqlConnection con, int seatNo);
        public List<Seat> getAllSeatsFromHallNo(SqlConnection con, int hallNo);
    }
}
