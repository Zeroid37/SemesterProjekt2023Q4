using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketVenueSystem.Model
{
    public class VenueEvent
    {
        public string id { get; set; }
        public double price { get; set; }
        public string eventName { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public Hall hall { get; set; }


        public VenueEvent() { }

        public VenueEvent(string id, double price, string eventName, DateTime startDate, DateTime endDate, Hall hall)
        {
            this.id = id;
            this.price = price;
            this.eventName = eventName;
            this.startDate = startDate;
            this.endDate = endDate;
            this.hall = hall;
        }
    }
}
