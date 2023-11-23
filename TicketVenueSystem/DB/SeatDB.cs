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
        public Seat getSeatFromSeatNo(int seatNo)
        {

            DBConnect DBC = DBConnect.getInstance();
            SqlConnection con = DBC.getConnection();

            SqlDataReader reader = null;
            String seatNumber = "";
            Boolean isInOrder = false;
            Seat seat = new Seat();
            using (con)
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = $"SELECT seatNumber, isInOrder from Seat where seatNumber={seatNo}";
                    reader = cmd.ExecuteReader();
                    while (reader.Read()) 
                    {
                        
                        seatNumber = reader.GetString(reader.GetOrdinal("seatNumber"));
                        isInOrder = reader.GetBoolean(reader.GetOrdinal("isInOrder"));

                        seat.seatNumber = seatNumber;
                        seat.isInOrder = isInOrder;
                    }
                    return seat;
                }
            }
        }
        public List<Seat> getAllSeatsFromHallNo(int hallNo)
        {

            DBConnect DBC = DBConnect.getInstance();
            SqlConnection con = DBC.getConnection();


            List<Seat> seats = new List<Seat>();
            SqlDataReader reader = null;
            String seatNumber = "";
            Boolean isInOrder = false;
            Seat seat = new Seat();
            using (con)
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = $"SELECT seatNumber, isInOrder from Seat where hallNumber_FK={hallNo}";
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        seatNumber = reader.GetString(reader.GetOrdinal("seatNumber"));
                        isInOrder = reader.GetBoolean(reader.GetOrdinal("isInOrder"));

                        seat.seatNumber = seatNumber;
                        seat.isInOrder = isInOrder;
                        seats.Add(seat);
                    }
                    return seats;
                }
            }
        }
    }
}
