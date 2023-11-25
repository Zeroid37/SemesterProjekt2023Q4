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
        /// <summary>
        /// Method to create a ticket
        /// </summary>
        /// <param name="seat">Desired Seat for the ticket</param>
        /// <param name="id">Id of the ticket</param>
        /// <param name="startDate">Start date of the ticket</param>
        /// <param name="endDate">End date of the ticket</param>
        /// <param name="user">User who tries to buy the ticket</param>
        /// <param name="venueEvent">Which event the ticket is for</param>
        /// <returns>Boolean</returns>

        public Boolean createTicket(Seat seat, string id, DateTime startDate, DateTime endDate, User user, VenueEvent venueEvent, Hall hall)
        {
            Boolean res = false;

            //Dummy Data
            DateTime dummyStartDate = new DateTime(2024, 10, 6);
            DateTime dummyEndDate = new DateTime(2024, 10, 7);

            if (!checkDateOverlap(startDate, endDate, venueEvent.startDate, venueEvent.endDate) &&
                (!checkSeatDateOverlap(startDate, endDate, dummyStartDate, dummyEndDate) &&
                (!checkHalldateOverlap(venueEvent.hall, startDate, endDate))
                {
                hall.venueEvents.Add(venueEvent);
                res = true;
            }

            return res;

        }
        /// <summary>
        /// Checks if the date of the ticket matches the date of the event
        /// </summary>
        /// <param name="desiredStartDate">Start date of the possible ticket</param>
        /// <param name="desiredEndDate">End date of the possible ticket</param>
        /// <param name="eventStartDate">Event start date</param>
        /// <param name="eventEndDate">Event end date</param>
        /// <returns>Boolean</returns>
        private Boolean checkDateOverlap(DateTime desiredStartDate, DateTime desiredEndDate, DateTime eventStartDate, DateTime eventEndDate)
        {
            Boolean res = true;

            if (desiredStartDate >= eventStartDate && desiredEndDate <= eventEndDate)
            {
                res = false;
            }
            return res;
        }

        /// <summary>
        /// Checks if a seat is available in the chosen time of the event
        /// </summary>
        /// <param name="desiredStartDate">Start date of the possible ticket</param>
        /// <param name="desiredEndDate">End date of the possible ticket</param>
        /// <param name="existingStartDate">Found seat date start</param>
        /// <param name="existingEndDate">Found seat date e</param>
        /// <returns></returns>
        private Boolean checkSeatDateOverlap(DateTime desiredStartDate, DateTime desiredEndDate, DateTime existingStartDate, DateTime existingEndDate)

        {
            Boolean res = true


            if (desiredStartDate < existingEndDate &&
                existingStartDate > desiredEndDate ||
                desiredStartDate > existingEndDate &&
                existingStartDate < desiredEndDate)
            {
                res = false;
            }
            return res;
        }

        private Boolean checkHalldateOverlap(Hall hall, DateTime desiredStartDate, DateTime desiredEndDate, DateTime existingStartDate, DateTime existingEndDate)
        {
            Boolean res = true;

            if (hall.venueEvents.Count >= 0)
            {
                if (desiredStartDate < existingEndDate &&
                existingStartDate > desiredEndDate ||
                desiredStartDate > existingEndDate &&
                existingStartDate < desiredEndDate)
                {
                    res = false;
                }

            }

            return res;
        }
    }
}

