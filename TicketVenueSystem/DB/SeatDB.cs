using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketVenueSystem.Model;

namespace TicketVenueSystem.DB
{
    public class SeatDB : SeatDAO
{
        private IConfiguration Configuration;
        private String? connectionString;

        public SeatDB(IConfiguration configuration) {
            Configuration = configuration;
            connectionString = Configuration.GetConnectionString("DefaultConnection");
        }
        /// <summary>
        /// Get a seat from it's seatnumber
        /// </summary>
        /// <param name="seatNo"></param>
        /// <returns>Seat</returns>
        public Seat getSeatFromSeatNo(string seatNo) {

            String getHallByHallNoQuery = "SELECT seatNumber, isInOrder from seat where seatNumber = @SEATNO";

            Seat seat = new Seat();
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(getHallByHallNoQuery, con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("SEATNO", seatNo);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    seat.seatNumber = reader.GetString(reader.GetOrdinal("seatNumber"));
                    seat.isInOrder = reader.GetBoolean(reader.GetOrdinal("isInOrder"));
                }
                return seat;
            }
        }
        /// <summary>
        /// Gets all seats associated with a hall by the hallNumber
        /// </summary>
        /// <param name="hallNo"></param>
        /// <returns>List of seats</returns>
        public List<Seat> getAllSeatsFromHallNo(string hallNo) {
            List<Seat> seats = new List<Seat>();

            String getSeatsByHallNoQuery = "SELECT seatNumber, isInOrder from Seat where hallNumber_FK = @HALLNO";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(getSeatsByHallNoQuery, con)) {
                con.Open();
                cmd.Parameters.AddWithValue("HALLNO", hallNo);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    String seatNumber = reader.GetString(reader.GetOrdinal("seatNumber"));
                    Boolean isInOrder = reader.GetBoolean(reader.GetOrdinal("isInOrder"));
                    Seat seat = new Seat(seatNumber, isInOrder);
                    seats.Add(seat);
                }
                return seats;
            }
        }
    }
}
