using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketVenueSystem.Model
{
    public class Ticket
    {
        public string ticket_ID { get; set; }
        public Seat seat { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public User user { get; set; }
        public VenueEvent venueEvent { get; set; }


        public Ticket() { }
        public Ticket (Seat seat, DateTime startDate, DateTime endDate, User user, VenueEvent venueEvent, string ticket_ID)
        {
            this.seat = seat;
            this.startDate = startDate;
            this.endDate = endDate;
            this.user = user;
            this.venueEvent = venueEvent;
            this.ticket_ID = ticket_ID; 
        }
    }
}
