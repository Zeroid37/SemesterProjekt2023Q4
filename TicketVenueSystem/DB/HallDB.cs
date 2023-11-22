using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketVenueSystem.Model;

namespace TicketVenueSystem.DB
{
    internal class HallDB : HallDAO
    {
        public Hall getHallFromHallNo( int hallNo)
        {
            DBConnect DBC = DBConnect.getInstance();
            SqlConnection con = DBC.getConnection();
            SqlDataReader reader = null;
            int hallNumber = 0;
            Hall hall = new Hall();
            using (con)
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = $"SELECT hallNumber from Hall where hallNumber={hallNo}";
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        hallNumber = reader.GetInt32(reader.GetOrdinal("hallNumber"));

                        hall.hallNumber = hallNumber;
                    }
                    return hall;
                }
            }
        }

        public List<Hall> getAllHallsFromHallNo(int hallNo)
        {
            DBConnect DBC = DBConnect.getInstance();
            SqlConnection con = DBC.getConnection();

            List<Hall> halls = new List<Hall>();
            SqlDataReader reader = null;
            int hallNumber = 0;
            Hall hall = new Hall();
            using (con)
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = $"SELECT seatNumber, isInOrder from Seat where hallNumber_FK={hallNo}";
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        hallNumber = reader.GetInt32(reader.GetOrdinal("hallNumber"));

                        hall.hallNumber = hallNumber;
                        halls.Add(hall);
                    }
                    return halls;
                }
            }
        }
    }
}
