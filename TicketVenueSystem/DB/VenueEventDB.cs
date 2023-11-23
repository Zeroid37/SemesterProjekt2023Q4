﻿using System.Data.SqlClient;
using TicketVenueSystem.Model;

namespace TicketVenueSystem.DB {
    internal class VenueEventDB : VenueEventDAO {
        public bool addVenueEventToDB(VenueEvent venueEvent) {
            DBConnect DBC = DBConnect.getInstance();
            SqlConnection con = DBC.getConnection();

            int insertedRowsNo = 0;
            string queryString = "insert into VenueEvent(venueEvent_ID, price, eventName, startDate, endDate, hallNumber_FK)" +
                                 "values(@VENUEEVENT_ID, @PRICE, @EVENTNAME, @STARTDATE, @ENDDATE, @HALLNUMBER_FK)";

            using (con) {
                using (SqlCommand cmd = new SqlCommand(queryString, con)) {
                    cmd.Parameters.AddWithValue("VENUEEVENT_ID", venueEvent.venueEvent_ID);
                    cmd.Parameters.AddWithValue("PRICE", venueEvent.price);
                    cmd.Parameters.AddWithValue("EVENTNAME", venueEvent.eventName);
                    cmd.Parameters.AddWithValue("STARTDATE", venueEvent.startDate);
                    cmd.Parameters.AddWithValue("ENDDATE", venueEvent.endDate);
                    cmd.Parameters.AddWithValue("HALLNUMBER_FK", venueEvent.hall.hallNumber);

                    insertedRowsNo = cmd.ExecuteNonQuery();
                }
            }
            return (insertedRowsNo > 0);
        }

        public VenueEvent getVenueEventById(String venueEvent_ID) {
            DBConnect DBC = DBConnect.getInstance();
            SqlConnection con = DBC.getConnection();

            VenueEvent ve = new VenueEvent();

            SqlDataReader reader = null;
            using (con) {
                using (SqlCommand cmd = con.CreateCommand()) {
                    cmd.CommandText = $"SELECT venueEvent_ID, price, eventName, startDate, endDate, hallNumber_FK from VenueEvent where venueEvent_ID = {venueEvent_ID}";
                    reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        ve.venueEvent_ID = reader.GetString(reader.GetOrdinal("venueEvent_ID"));
                        ve.price = reader.GetDouble(reader.GetOrdinal("price"));
                        ve.eventName = reader.GetString(reader.GetOrdinal("eventName"));
                        ve.startDate = reader.GetDateTime(reader.GetOrdinal("startDate"));
                        ve.endDate = reader.GetDateTime(reader.GetOrdinal("endDate"));

                        HallDB halldb = new HallDB();
                        Hall hall = halldb.getHallFromHallNo(reader.GetString(reader.GetOrdinal("hallnumber_fk")));

                        ve.hall = hall;
                    }
                }
            }
            return ve;
        }
    }
}
