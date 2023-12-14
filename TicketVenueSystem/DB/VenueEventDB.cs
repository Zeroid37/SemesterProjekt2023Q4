using System.Data.SqlClient;
using TicketVenueSystem.Model;
using Microsoft.Extensions.Configuration;
using System.Transactions;

namespace TicketVenueSystem.DB {
    public class VenueEventDB : VenueEventDAO {

        private IConfiguration Configuration;
        private String? connectionString;

        public VenueEventDB (IConfiguration configuration)
        {
            Configuration = configuration;
            connectionString = Configuration.GetConnectionString("DefaultConnection");
        }

        public Boolean addVenueEventToDB(VenueEvent venueEvent) {

            int insertedRowsNo = 0;
            string addVenueEventToDBQueryString = "insert into VenueEvent(venueEvent_ID, price, eventName, startDate, endDate, hallNumber_FK, organizerId_FK)" +
                                 "values(@VENUEEVENT_ID, @PRICE, @EVENTNAME, @STARTDATE, @ENDDATE, @HALLNUMBER_FK, @ORGANIZERID_FK)";

            using (SqlConnection con = new SqlConnection(connectionString))   
            using (SqlCommand cmd = new SqlCommand(addVenueEventToDBQueryString, con)) {
                con.Open();
                cmd.Parameters.AddWithValue("VENUEEVENT_ID", venueEvent.venueEvent_ID);
                cmd.Parameters.AddWithValue("PRICE", venueEvent.price);
                cmd.Parameters.AddWithValue("EVENTNAME", venueEvent.eventName);
                cmd.Parameters.AddWithValue("STARTDATE", venueEvent.startDate);
                cmd.Parameters.AddWithValue("ENDDATE", venueEvent.endDate);
                cmd.Parameters.AddWithValue("HALLNUMBER_FK", venueEvent.hall.hallNumber);
                cmd.Parameters.AddWithValue("ORGANIZERID_FK", venueEvent.eventOrganizer.organizerId);


                insertedRowsNo = cmd.ExecuteNonQuery();
            }
            return (insertedRowsNo > 0);
        }

        public VenueEvent getVenueEventById(int venueEvent_ID) {

            VenueEvent ve = new VenueEvent();
            SqlDataReader reader = null;


            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = con.CreateCommand())
            {
                con.Open();
                cmd.CommandText = $"SELECT venueEvent_ID, price, eventName, startDate, endDate, hallNumber_FK from VenueEvent where venueEvent_ID = {venueEvent_ID}";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ve.venueEvent_ID = reader.GetString(reader.GetOrdinal("venueEvent_ID"));
                    ve.price = reader.GetDouble(reader.GetOrdinal("price"));
                    ve.eventName = reader.GetString(reader.GetOrdinal("eventName"));
                    ve.startDate = reader.GetDateTime(reader.GetOrdinal("startDate"));
                    ve.endDate = reader.GetDateTime(reader.GetOrdinal("endDate"));

                    HallDB halldb = new HallDB(Configuration);
                    Hall hall = halldb.getHallFromHallNo(reader.GetString(reader.GetOrdinal("hallnumber_fk")));

                    ve.hall = hall;
                }
            }
            return ve;
        }

        public List<VenueEvent> getAllVenueEvents()
        {
            List<VenueEvent> venueEventsList = new List<VenueEvent>();
            String getVenueEvents = "SELECT venueEvent_ID, price, eventName, startDate, endDate, hallNumber_FK, organizerId_FK from venueEvent";
            HallDAO hdb = new HallDB(Configuration);
            UserDAO udb = new UserDB(Configuration);
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(getVenueEvents, con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        String venueEvent_ID = reader.GetString(reader.GetOrdinal("venueEvent_ID"));
                        Double price = reader.GetDouble(reader.GetOrdinal("price"));
                        String eventName = reader.GetString(reader.GetOrdinal("eventName"));
                        DateTime startDate = reader.GetDateTime(reader.GetOrdinal("startDate"));
                        DateTime endDate = reader.GetDateTime(reader.GetOrdinal("endDate"));

                        String hallNumber_FK = reader.GetString(reader.GetOrdinal("hallNumber_FK"));
                        Hall hall = hdb.getHallFromHallNo(hallNumber_FK);

                        String eventOrgId = reader.GetString(reader.GetOrdinal("organizerId_FK"));
                        EventOrganizer eOrg = udb.getEventOrganizerByID(eventOrgId);

                        VenueEvent ve = new VenueEvent(venueEvent_ID, price, eventName, startDate, endDate, hall, eOrg);
                        venueEventsList.Add(ve);
                    }
                }
            }
            return venueEventsList;
        }
    }
}
