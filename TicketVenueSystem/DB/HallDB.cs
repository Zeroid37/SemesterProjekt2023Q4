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
            connectionString = Configuration.GetConnectionString("DefaultConnection");
        }
        /// <summary>
        /// Get a hall from the database by it's hallNumber
        /// </summary>
        /// <param name="hallNo"></param>
        /// <returns>Hall</returns>
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
    }
}