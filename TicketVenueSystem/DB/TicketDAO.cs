using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketVenueSystem.Model;

namespace TicketVenueSystem.DB
{
    public interface TicketDAO
    {
        public int getTicketCount();
        public Boolean addTicketToDB(Ticket ticket, int ticketCount);
        public Boolean removeTicketFromDB(Ticket ticket);
        public Ticket getTicketByUserID(string userID);
        public List<Ticket> getAllTicketsBySeatNo(String seatNo);
    }
}
