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
        public Hall getHallFromHallNo(SqlConnection con, int hallNo)
        {
            SqlDataReader reader = null;
            int hallNumber = 0;
            Hall hall = null;
            using (con)
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = $"SELECT seatNumber, isInOrder from Seat where seatNumber={hallNo}";
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

        public List<Hall> getAllHallsFromHallNo(SqlConnection con, int hallNo)
        {
            List<Hall> halls = new List<Hall>();
            SqlDataReader reader = null;
            int hallNumber = 0;
            Hall hall = null;
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
