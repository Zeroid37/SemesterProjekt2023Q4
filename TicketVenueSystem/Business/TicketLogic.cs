using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketVenueSystem.Model;

namespace TicketVenueSystem.Business
{
    public class TicketLogic
    {
        public Boolean createTicket(Seat seat, string id, DateTime startDate, DateTime endDate, User user, VenueEvent venueEvent)
        {
            Boolean res = false;

            Ticket ticket = null; ;

            //Dummy Data
            DateTime dummyStartDate = new DateTime(2024, 10, 6);
            DateTime dummyEndDate = new DateTime(2024, 10, 7);


            if (!checkDateOverlap(startDate, endDate, venueEvent.startDate, venueEvent.endDate))
            {
                if (!checkSeatDateOverlap(startDate, endDate, dummyStartDate, dummyEndDate))
                {
                    res = true;
                }
            }
            return res;
        }

        private Boolean checkDateOverlap(DateTime desiredStartDate, DateTime desiredEndDate, DateTime eventStartDate, DateTime eventEndDate)
        {
            Boolean res = true;

            if (desiredStartDate >= eventStartDate && desiredEndDate <= eventEndDate)
            {
                res = false;
            }

            return res;
        }

        private Boolean checkSeatDateOverlap(DateTime desiredStartDate, DateTime desiredEndDate, DateTime existingStartDate, DateTime existingEndDate)
        {
            Boolean res = true;

            if (desiredStartDate < existingEndDate && existingStartDate > desiredEndDate || desiredStartDate > existingEndDate && existingStartDate < desiredEndDate)
            {
                res = false;
            }
                return res;

        }
    }
}
