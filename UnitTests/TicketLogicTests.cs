using TicketVenueSystem.Business;
using TicketVenueSystem.Model;

namespace UnitTests
{
    public class TicketLogicTests
    {
        [Fact]
        public void TestEventDates()
        {
            TicketLogic tl = new TicketLogic();
            double price = 199.95;
            string eventName = "Koncert";
            DateTime startDate = new DateTime(2024, 10, 5);
            DateTime endDate = startDate.AddDays(2);
            Hall hallone = null;
            VenueEvent venueEvent = new VenueEvent(price, eventName, startDate, endDate, hallone);
            Seat seat = null;
            User user = null;

            //Test1 Dates
            DateTime unacceptableDateStart = new DateTime(2024, 10, 4);
            DateTime unacceptableDateEnd = new DateTime(2024, 10, 6);

            //Test2 Dates
            DateTime acceptableDateStart = new DateTime(2024, 10, 5);
            DateTime acceptableDateEnd = new DateTime(2024, 10, 5);

            Boolean test1 = tl.createTicket(seat, "1", unacceptableDateStart, unacceptableDateEnd, user, venueEvent); //Expected Flase
            Boolean test2 = tl.createTicket(seat, "2", acceptableDateStart, acceptableDateEnd, user, venueEvent); //Expected True

            Assert.False(test1);
            Assert.True(test2);
        }
        [Fact]
        public void TestSeatOverlap()
        {
            TicketLogic tl = new TicketLogic();
            double price = 199.95;
            string eventName = "Koncert";
            DateTime startDate = new DateTime(2024, 10, 5);
            DateTime endDate = startDate.AddDays(2);
            Hall hallone = null;
            VenueEvent venueEvent = new VenueEvent(price, eventName, startDate, endDate, hallone);
            Seat seat = null;
            User user = null;

            //Test1 Dates
            DateTime unacceptableDateStart = new DateTime(2024, 10, 5);
            DateTime unacceptableDateEnd = new DateTime(2024, 10, 6);

            //Test2 Dates
            DateTime acceptableDateStart = new DateTime(2024, 10, 5);
            DateTime acceptableDateEnd = new DateTime(2024, 10, 5);

            Boolean test1 = tl.createTicket(seat, "3", unacceptableDateStart, unacceptableDateEnd, user, venueEvent); //Expected Flase
            Boolean test2 = tl.createTicket(seat, "4", acceptableDateStart, acceptableDateEnd, user, venueEvent); //Expected True


            Assert.False(test1);
            Assert.True(test2);
        }
    }
}