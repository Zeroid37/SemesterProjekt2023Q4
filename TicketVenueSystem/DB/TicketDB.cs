using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketVenueSystem.Model;
using System.Threading;

namespace TicketVenueSystem.DB
{
    public class TicketDB : TicketDAO
    {
        private IConfiguration Configuration;
        private String? connectionString;

        public TicketDB(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionString = Configuration.GetConnectionString("DefaultConnection");
        }
        /// <summary>
        /// Adds a ticket to the database
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="ticketCount"></param>
        /// <returns>Boolean true or false</returns>
        public bool addTicketToDB(Ticket ticket, int ticketCount)
        {
            int insertedRowsNo = 0;
            string queryString = "if (Select Count(*) as count_ticket from Ticket) = @TICKETCOUNT " +
                                 "insert into Ticket(ticket_ID, startDate, endDate, venueEventID_FK, userID_FK, seatNumber_FK)" +
                                 "values(@TICKET_ID, @STARTDATE, @ENDDATE, @VENUEEVENTID_FK, @USERID_FK, @SEATNUMBER_FK)";
            using (SqlConnection con = new SqlConnection(connectionString))                
            using (SqlCommand cmd = new SqlCommand(queryString, con))
            {
                Thread.Sleep(10000);
                con.Open();
                cmd.Parameters.AddWithValue("TICKETCOUNT", ticketCount);
                cmd.Parameters.AddWithValue("TICKET_ID", ticket.ticket_ID);
                cmd.Parameters.AddWithValue("STARTDATE", ticket.startDate);
                cmd.Parameters.AddWithValue("ENDDATE", ticket.endDate);
                cmd.Parameters.AddWithValue("VENUEEVENTID_FK", ticket.venueEvent.venueEvent_ID);
                cmd.Parameters.AddWithValue("USERID_FK", ticket.user.userId);
                cmd.Parameters.AddWithValue("SEATNUMBER_FK", ticket.seat.seatNumber);

                insertedRowsNo = cmd.ExecuteNonQuery();
            }
            return (insertedRowsNo > 0);
        }

        /// <summary>
        /// Get all tickets by the seat associated with the tickets through it's seat number
        /// </summary>
        /// <param name="seatNo"></param>
        /// <returns>List of Tickets</returns>
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

        /// <summary>
        /// Get number of tickets in the database
        /// </summary>
        /// <returns>int</returns>
        public int getTicketCount()
        {
            int ticketCount = -1;
            String getSeatsByHallNoQuery = "Select Count(*) as count_ticket from Ticket";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(getSeatsByHallNoQuery, con))
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ticketCount = reader.GetInt32(reader.GetOrdinal("count_ticket"));
                }
                return ticketCount;
            }
        }

        
        public bool removeTicketFromDB(Ticket ticket)
        {
            throw new NotImplementedException();
        }
    }
}
