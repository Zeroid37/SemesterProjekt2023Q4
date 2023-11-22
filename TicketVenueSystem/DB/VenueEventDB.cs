using System.Data.SqlClient;
using TicketVenueSystem.Model;

namespace TicketVenueSystem.DB {
    internal class VenueEventDB : VenueEventDAO {
        public bool addVenueEventToDB(string id, double price, string eventName, DateTime startDate, DateTime endDate, Hall hall) {
            DBConnect DBC = DBConnect.getInstance();
            SqlConnection con = DBC.getConnection();

            int insertedRowsNo = 0;
            string queryString = "insert into VenueEvent(id, price, eventName, startDate, endDate, hallNumber_FK)" +
                                 "values(@ID, @PRICE, @EVENTNAME, @STARTDATE, @ENDDATE, @HALLNUMBER_FK)";

            using (con) {
                using (SqlCommand cmd = new SqlCommand(queryString, con)) {
                    cmd.Parameters.AddWithValue("ID", id);
                    cmd.Parameters.AddWithValue("PRICE", price);
                    cmd.Parameters.AddWithValue("EVENTNAME", eventName);
                    cmd.Parameters.AddWithValue("STARTDATE", startDate);
                    cmd.Parameters.AddWithValue("ENDDATE", endDate);
                    cmd.Parameters.AddWithValue("HALLNUMBER_FK", hall.hallNumber);

                    insertedRowsNo = cmd.ExecuteNonQuery();
                }
            }
            return (insertedRowsNo > 0);
        }

        public VenueEvent getVenueEventById(string id) {
            DBConnect DBC = DBConnect.getInstance();
            SqlConnection con = DBC.getConnection();

            VenueEvent ve = new VenueEvent();
            using (con) {
                using (SqlCommand cmd = con.CreateCommand()) {
                    cmd.CommandText = $"SELECT (id, price, eventName, startDate, endDate, hallNumber_FK) from VenueEvent where id = {id}";
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        ve.id = reader.GetString(reader.GetOrdinal("id"));
                        ve.price = reader.GetDouble(reader.GetOrdinal("price"));
                        ve.eventName = reader.GetString(reader.GetOrdinal("eventName"));
                        ve.startDate = reader.GetDateTime(reader.GetOrdinal("startDate"));
                        ve.endDate = reader.GetDateTime(reader.GetOrdinal("endDate"));

                        HallDB halldb = new HallDB();
                        Hall hall = halldb.getHallFromHallNo(reader.GetInt32(reader.GetOrdinal("hallnumber_fk")));

                        ve.hall = hall;
                    }
                }
            }
            return ve;
        }
    }
}
