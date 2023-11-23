using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketVenueSystem.Model
{
    public class Seat
    {
        public string seatNumber { get; set; }
        public Boolean isInOrder { get; set; }


        public Seat()
        {
            this.isInOrder = true;
        }
        public Seat(string seatNumber, Boolean isInOrder)
        { 
            this.seatNumber = seatNumber;
            this.isInOrder = isInOrder;
        }

    }
}
