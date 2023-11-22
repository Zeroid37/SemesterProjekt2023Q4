using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketVenueSystem.Model
{
    public class Seat
    {
        public int seatNumber { get; set; }
        public Boolean isInOrder { get; set; }


        public Seat()
        {
            this.isInOrder = true;
        }
        public Seat(int seatNumber, Boolean isInOrder)
        { 
            this.seatNumber = seatNumber;
            this.isInOrder = isInOrder;
        }

    }
}
