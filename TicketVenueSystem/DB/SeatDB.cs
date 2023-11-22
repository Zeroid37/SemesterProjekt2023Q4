using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketVenueSystem.Model;

namespace TicketVenueSystem.DB
{
    internal class SeatDB : SeatDAO
    {
        public Seat createObject(SqlConnection con, int seatNo)
        {
            SqlDataReader reader = null;
            int seatNumber = 0;
            Boolean isInOrder = false;
            Seat seat = null;
            using (con)
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = $"SELECT seatNumber, isInOrder from Seat where seatNumber = {seatNo}";
                    reader = cmd.ExecuteReader();
                    while (reader.Read()) 
                    {
                        
                        seatNumber = reader.GetInt32(reader.GetOrdinal("seatNumber"));
                        isInOrder = reader.GetBoolean(reader.GetOrdinal("isInOrder"));

                        seat.seatNumber = seatNumber;
                        seat.isInOrder = isInOrder;
                    }
                    return seat;
                }
            }
        }
    }
}
