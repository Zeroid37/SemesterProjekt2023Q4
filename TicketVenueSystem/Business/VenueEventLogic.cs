using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketVenueSystem.DB;
using TicketVenueSystem.Model;

namespace TicketVenueSystem.Business
{
    internal class VenueEventLogic
    {



        public Boolean createVenueEvent(double price, string eventName, DateTime startDate, DateTime endDate, Hall hall, EventOrganizer eventOrganizer, VenueEvent venueEvent)
        {

            DateTime dummyStartDate = new DateTime(2023, 1, 13);
            DateTime dummyEndDate = new DateTime(2023, 1, 14);

            DateTime dummyExistingStartDate = new DateTime(2023, 1, 20);
            DateTime dummyExistingEndDate = new DateTime(2023, 1, 21);

            //string dummyHallNo = "8";
            //string dummyExistingHallNo = "1";

            Boolean isHallDateOverlap = checkHallDateOverlap(startDate, endDate, venueEvent.startDate, venueEvent.endDate);
            Boolean isHallOverlap = checkHallOverlap(hall.hallNumber, venueEvent.hall.hallNumber);

            if (isHallDateOverlap)
            {
                return isHallOverlap;
            }
            return (isHallDateOverlap && isHallOverlap);
        }

        private Boolean checkHallOverlap(string hall, string existingHall)
        {
            Boolean res = true;
            //List<int> overlap = new List<int> { 1, 3, 8 };
            List<string> existingHalls = new List<string>(DBConnect);


            for (int ol = 0; ol < existingHalls.Count; ol++)
            {
                if (existingHalls[ol] == hall)
                {
                    res = true;
                    break;
                }
                else
                {
                    res = false;
                }
            }
            return res;
        }

        private Boolean checkHallDateOverlap(DateTime desiredStartDate, DateTime desiredEndDate, DateTime existingStartDate, DateTime existingEndDate)
        {
            Boolean res = true;


            if (desiredStartDate < existingEndDate &&
            existingStartDate > desiredEndDate ||
            desiredStartDate > existingEndDate &&
            existingStartDate < desiredEndDate)
            {
                res = false;
            }



            return res;
        }
    }
}
