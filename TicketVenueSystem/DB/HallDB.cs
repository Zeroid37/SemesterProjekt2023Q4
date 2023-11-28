using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TicketVenueSystem.Model;

namespace TicketVenueSystem.DB
{
    public class HallDB : HallDAO
    {
        private IConfiguration Configuration;
        private String? connectionString;

        public HallDB(IConfiguration configuration) {
            Configuration = configuration;
            connectionString = Configuration.GetConnectionString("ConnectMsSqlString");
        }

        public Hall getHallFromHallNo(string hallNo) {
            SqlDataReader reader = null;
            Hall hall = new Hall();
            SeatDB seatdb = new SeatDB(Configuration);

            String getHallByHallNoQuery = "SELECT hallNumber from Hall where hallNumber = @HALLNO";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(getHallByHallNoQuery, con)) {
                con.Open();
                cmd.Parameters.AddWithValue("HALLNO", hallNo);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    hall.hallNumber = reader.GetString(reader.GetOrdinal("hallNumber"));
                    hall.seats = seatdb.getAllSeatsFromHallNo(hallNo);
                }
                return hall;
                }
            }
        
        public List<Hall> getAllHallsFromHallNo(string hallNo) //TODO Update
        {
            DBConnect DBC = DBConnect.getInstance();
            SqlConnection con = DBC.getConnection();

            List<Hall> halls = new List<Hall>();
            SqlDataReader reader = null;
            string hallNumber = "";
            Hall hall = new Hall();
            using (con)
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = $"SELECT seatNumber, isInOrder from Seat where hallNumber_FK={hallNo}";
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        hallNumber = reader.GetString(reader.GetOrdinal("hallNumber"));

                        hall.hallNumber = hallNumber;
                        halls.Add(hall);
                    }
                    return halls;
                }
            }
        }
    }
}