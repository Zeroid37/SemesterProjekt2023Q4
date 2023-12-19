using Microsoft.Extensions.Configuration;
using TicketVenueSystem.Business;
using TicketVenueSystem.Model;
using Xunit.Abstractions;

namespace UnitTests
{
    public class TicketLogicTests
    {
        private readonly ITestOutputHelper _extraOutput;
        private readonly IConfiguration inConfig;

        private TicketLogic _ticketLogic;
        private VenueEventLogic _venueEventLogic;
        private UserLogic _userLogic;

        public TicketLogicTests(ITestOutputHelper Output)
        {
            _extraOutput = Output;
            inConfig = TestConfigHelper.GetIConfigurationRoot();
            _ticketLogic = new TicketLogic(inConfig);
            _venueEventLogic = new VenueEventLogic(inConfig);
            _userLogic = new UserLogic(inConfig);
        }

        [Fact]
        public void TestValidateTicketVALID()
        {
            Ticket ticket = new Ticket();
            //Vi henter en eksisterende venue event fra databasen som indeholder
            //de nødvændige objekter til testen.
            VenueEvent venueEvent = _venueEventLogic.getVenueEventById(1);

            String ticket_ID = "5000";
            //Der er på nuværende tidspunkt ingen bookings på dette seat, dermed må det være gyldigt
            Seat seat = venueEvent.hall.seats[3];
            //Vi henter datoerne fra eventet så vi er sikre på at vores test datoer
            //Overholder event datoerne.
            DateTime startDate = venueEvent.startDate;
            DateTime endDate = startDate.AddDays(1);

            //Her henter vi en bruger som vi ved findes i systemet allerede
            User user = _userLogic.getUserByEmail("Kasper@mail");

            //Ticket bliver fyldt med overstående data
            ticket.ticket_ID = ticket_ID;
            ticket.seat = seat;
            ticket.startDate = startDate;
            ticket.endDate = endDate;
            ticket.user = user;
            ticket.venueEvent = venueEvent;

            List<Ticket> allTickets = _ticketLogic.getAllTicketsBySeatNo(seat.seatNumber);

            Boolean ticketValidation = _ticketLogic.validateTicket(allTickets, ticket);
            Assert.True(ticketValidation);
            _extraOutput.WriteLine(ticketValidation.ToString() + "For Valid test");
        }

        [Fact]
        public void TestValidateTicketINVALID()
        {
            Ticket ticket = new Ticket();
            //Vi henter en eksisterende venue event fra databasen som indeholder
            //de nødvændige objekter til testen.
            VenueEvent venueEvent = _venueEventLogic.getVenueEventById(1);

            String ticket_ID = "5000";
            //Der er på nuværende tidspunkt en booking på dette seat
            //hvis tidspunkt bliver ramt af det nye ticket
            Seat seat = venueEvent.hall.seats[0];
            //Vi henter datoerne fra eventet så vi er sikre på at vores test datoer
            //Overholder event datoerne.
            DateTime startDate = venueEvent.startDate;
            DateTime endDate = startDate.AddDays(1);

            //Her henter vi en bruger som vi ved findes i systemet allerede
            User user = _userLogic.getUserByEmail("Kasper@mail");

            //Ticket bliver fyldt med overstående data
            ticket.ticket_ID = ticket_ID;
            ticket.seat = seat;
            ticket.startDate = startDate;
            ticket.endDate = endDate;
            ticket.user = user;
            ticket.venueEvent = venueEvent;

            List<Ticket> allTickets = _ticketLogic.getAllTicketsBySeatNo(seat.seatNumber);

            Boolean ticketValidation = _ticketLogic.validateTicket(allTickets, ticket);
            Assert.False(ticketValidation);
            _extraOutput.WriteLine(ticketValidation.ToString() + "For Invalid test");

        }
    }
}