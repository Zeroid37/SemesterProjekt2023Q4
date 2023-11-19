using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketVenueSystem.Model
{
    public class Hall
    {
        public int hallNumber { get; set; }
        public List<Seat> seats { get; set; }

        public Hall(int hallNumber)
        {
            this.hallNumber = hallNumber;
            seats = new List<Seat>();
        }

        public Boolean addSeat(Seat seat)
        {
            Boolean res = false;
            if(!seats.Contains(seat))
            {
                seats.Add(seat);
                res = true;
            }
            return res;
        }

        public Boolean removeSeat(Seat seat)
        {
            Boolean res = false;
            if(seats.Contains(seat))
            { 
                seats.Remove(seat);
                res = true;
            }
            return res;
        }
    }
}
