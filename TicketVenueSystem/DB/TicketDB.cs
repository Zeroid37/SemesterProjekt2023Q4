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
    public class TicketDB : TicketDAO
    {
        private IConfiguration Configuration;
        private String? connectionString;

        public TicketDB(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionString = Configuration.GetConnectionString("ConnectMsSqlString");
        }

        public bool addTicketToDB(Ticket ticket)
        {
            DBConnect DBC = DBConnect.getInstance();
            SqlConnection con = DBC.getConnection();

            int insertedRowsNo = 0;
            string queryString = "insert into Ticket(ticket_ID, startDate, endDate, venueEventID_FK, userID_FK, seatNumber_FK)" +
                                 "values(@TICKET_ID, @STARTDATE, @ENDDATE, @VENUEEVENTID_FK, @USERID_FK, @SEATNUMBER_FK)";

            using (con)
            {
                using (SqlCommand cmd = new SqlCommand(queryString, con))
                {
                    cmd.Parameters.AddWithValue("TICKET_ID", ticket.ticket_ID);
                    cmd.Parameters.AddWithValue("STARTDATE", ticket.startDate);
                    cmd.Parameters.AddWithValue("ENDDATE", ticket.endDate);
                    cmd.Parameters.AddWithValue("VENUEEVENTID_FK", ticket.venueEvent.venueEvent_ID);
                    cmd.Parameters.AddWithValue("USERID_FK", ticket.user.userId);
                    cmd.Parameters.AddWithValue("SEATNUMBER_FK", ticket.seat.seatNumber);

                    insertedRowsNo = cmd.ExecuteNonQuery();
                }
            }
            return (insertedRowsNo > 0);
        }

        public List<Ticket> getAllTicketsBySeatNo(string seatNo)
        {
            List<Ticket> ticketList = new List<Ticket>();
            String getTicketsFromSeatNoQuery = "SELECT ticket_ID, startDate, endDate, venueEventID_FK, userID_FK from Ticket where seatNumber_FK = @SEATNO";

            VenueEventDB vedb = new VenueEventDB(Configuration);
            UserDB udb = new UserDB(Configuration);
            SeatDB sdb = new SeatDB(Configuration);


            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(getTicketsFromSeatNoQuery, con))
                {
                    cmd.Parameters.AddWithValue("SEATNO", seatNo);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Ticket ticket = new Ticket();

                        String ticketID = reader.GetString(reader.GetOrdinal("ticket_ID"));
                        DateTime startDate = reader.GetDateTime(reader.GetOrdinal("startDate"));
                        DateTime endDate = reader.GetDateTime(reader.GetOrdinal("endDate"));
                        ticket.ticket_ID = ticketID;
                        ticket.startDate = startDate;
                        ticket.endDate = endDate;
                        ticket.seat = sdb.getSeatFromSeatNo(seatNo);
                        ticket.user = udb.getUserByUserID(reader.GetString(reader.GetOrdinal("userID_FK")));
                        ticket.venueEvent = vedb.getVenueEventById(reader.GetInt32(reader.GetOrdinal("venueEventID_FK")));

                        ticketList.Add(ticket);
                    }
                }
            }
            return ticketList;
        }

        public Ticket getTicketByUserID(string userID)
        {
            DBConnect DBC = DBConnect.getInstance();
            SqlConnection con = DBC.getConnection();

            Ticket ticket = new Ticket();

            SqlDataReader reader = null;
            using (con)
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = $"SELECT ticket_ID, startDate, endDate, venueEventID_FK, userID_FK, seatNumber_FK from Ticket where userID_FK = {userID}";
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        //UserDB udb = new UserDB();
                        //VenueEventDB vedb = new VenueEventDB();
                        //SeatDB sdb = new SeatDB();

                        ticket.ticket_ID = reader.GetString(reader.GetOrdinal("ticket_ID"));
                        ticket.startDate = reader.GetDateTime(reader.GetOrdinal("startDate"));
                        ticket.endDate = reader.GetDateTime(reader.GetOrdinal("endDate"));

                        //ticket.seat = sdb.getSeatFromSeatNo(reader.GetString(reader.GetOrdinal("seatNumber_FK")));
                        //ticket.venueEvent = vedb.getVenueEventById(reader.GetString(reader.GetOrdinal("venueEventID_FK")));
                        
                        //ticket.user = udb.getUserByUserID(reader.GetString(reader.GetOrdinal("userID_FK")));                       
                            
 
                    }
                }
            }
            return ticket;
        }

        public bool removeTicketFromDB(Ticket ticket)
        {
            throw new NotImplementedException();
        }
    }
}
