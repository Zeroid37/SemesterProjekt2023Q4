using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketVenueSystem.Model
{
    public class Ticket
    {
        public Seat seat { get; set; }
        public string id { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public User user { get; set; }
        public VenueEvent venueEvent { get; set; }


        public Ticket() { }
        public Ticket (Seat seat, string id, DateTime startDate, DateTime endDate, User user, VenueEvent venueEvent)
        {
            this.seat = seat;
            this.id = id;
            this.startDate = startDate;
            this.endDate = endDate;
            this.user = user;
            this.venueEvent = venueEvent;
        }
    }
}
