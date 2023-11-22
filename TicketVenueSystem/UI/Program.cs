using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TicketVenueSystem.Business;
using TicketVenueSystem.DB;
using TicketVenueSystem.Model;

namespace TicketVenueSystem.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HallDB hdb = new HallDB();
            SeatDB sdb = new SeatDB();
            List<Seat> seats;

            Hall hall = hdb.getHallFromHallNo(1);
            Console.WriteLine(hall.hallNumber);

            Seat seat = sdb.getSeatFromSeatNo(hall.hallNumber);
            Console.WriteLine(seat.seatNumber);



            //seats = sdb.getAllSeatsFromHallNo(con, hall.hallNumber);

            //foreach (Seat seat in seats)
            //{
            //    Console.WriteLine(seat);
            //}




        }

        private void testTicketWithDummy()
        {
            TicketLogic tl = new TicketLogic();
            //Create User
            string id = "69";
            string firstName = "Smajo";
            string lastName = "Omanovic";
            string email = "Smajo@mail";
            string phoneNo = "1234567890";
            DateTime dateOfBirth = new DateTime(2000, 1, 16);
            User user = new User(id, firstName, lastName, email, phoneNo, dateOfBirth);


            //Create Hall & Seats
            int hallNumber = 1;
            Hall hallOne = new Hall(hallNumber);
            hallOne.addSeat(new Seat(1, true));
            hallOne.addSeat(new Seat(2, true));
            hallOne.addSeat(new Seat(3, true));
            hallOne.addSeat(new Seat(4, true));



            //Create VenueEvent
            double price = 199.95;
            string eventName = "Koncert";
            DateTime startDate = new DateTime(2024, 10, 5);
            DateTime endDate = startDate.AddDays(2);
            VenueEvent venueEvent = new VenueEvent(price, eventName, startDate, endDate, hallOne);


            while (true)
            {
                Console.WriteLine("Welcome to Ticket Vendor 3000");
                Console.WriteLine("Please choose an event from the available ones:");
                Console.WriteLine("1. " + venueEvent.eventName);
                string input = Console.ReadLine();
                VenueEvent chosenEvent = venueEvent;
                Console.WriteLine("Event information:");
                Console.WriteLine("Name: " + chosenEvent.eventName);
                Console.WriteLine("Price: " + chosenEvent.price);
                Console.WriteLine("Start date: " + chosenEvent.startDate.ToString("dd/MM/yyyy"));
                Console.WriteLine("End date: " + chosenEvent.endDate.ToString("dd/MM/yyyy"));
                Console.WriteLine("Hall: " + chosenEvent.hall.hallNumber);

                Console.WriteLine("\nCurrent seats: ");
                foreach (Seat s in chosenEvent.hall.seats)
                {
                    Console.WriteLine(s.seatNumber);
                }

                Console.Write("Please choose a seat: ");
                string seatNumber = Console.ReadLine();
                int seatNumberInt = Int32.Parse(seatNumber);
                Seat chosenSeat = null;

                Boolean found = false;
                int index = 0;
                while (!found && index < chosenEvent.hall.seats.Count)
                {
                    if (seatNumberInt == chosenEvent.hall.seats[index].seatNumber)
                    {
                        chosenSeat = chosenEvent.hall.seats[index];
                        found = true;
                    }
                    index++;
                }


                Console.WriteLine("Available dates: ");
                Console.WriteLine(chosenEvent.startDate.ToString("dd/MM/yyyy"));
                Console.WriteLine(chosenEvent.startDate.AddDays(1).ToString("dd/MM/yyyy"));
                Console.WriteLine(chosenEvent.startDate.AddDays(2).ToString("dd/MM/yyyy"));
                Console.Write("Please choose a start date: ");
                string startDateInput = Console.ReadLine();
                Console.Write("Please choose an end date: ");
                string endDateInput = Console.ReadLine();

                DateTime startDateTicket = DateTime.ParseExact(startDateInput, "dd/MM/yyyy", null);
                DateTime endDateTicket = DateTime.ParseExact(endDateInput, "dd/MM/yyyy", null);

                tl.createTicket(chosenSeat, "69", startDateTicket, endDateTicket, user, venueEvent);
            }
        }
    }
}
