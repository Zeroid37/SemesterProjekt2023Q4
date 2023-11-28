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
            connectionString = Configuration.GetConnectionString("ConnectMsSqlString");
        }

        public Seat getSeatFromSeatNo(string seatNo) { //TODO Update

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

        public List<Seat> getAllSeatsFromHallNo(string hallNo) {
            List<Seat> seats = new List<Seat>();
            SqlDataReader reader = null;
            String seatNumber = "";
            Boolean isInOrder = false;
            Seat seat = new Seat();

            String getSeatsByHallNoQuery = "SELECT seatNumber, isInOrder from Seat where hallNumber_FK = @HALLNO";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(getSeatsByHallNoQuery, con)) {
                con.Open();
                cmd.Parameters.AddWithValue("HALLNO", hallNo);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    seat.seatNumber = reader.GetString(reader.GetOrdinal("seatNumber"));
                    seat.isInOrder = reader.GetBoolean(reader.GetOrdinal("isInOrder"));
                    seats.Add(seat);
                }
                return seats;
            }
        }
    }
}
