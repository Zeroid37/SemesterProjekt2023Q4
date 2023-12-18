﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketVenueSystem.DB;
using TicketVenueSystem.Model;

namespace TicketVenueSystem.Business
{
    public class TicketLogic
    {
        private IConfiguration Configuration;

        public TicketLogic(IConfiguration configuration) {
            Configuration = configuration;
        }

        public int getTicketCount()
        {
            int count = -1;
            TicketDAO tdb = new TicketDB(Configuration);
            count = tdb.getTicketCount();

            return count;
        }

        public Ticket createTicketFromForm(int venueEventId, string seatNo, string userEmail, DateTime startDate, DateTime endDate) {
            //DELETE THIS
            Random rnd = new Random();
            int num = rnd.Next();

            VenueEventDB vedb = new VenueEventDB(Configuration);
            SeatDB sdb = new SeatDB(Configuration);
            UserDB udb = new UserDB(Configuration);

            VenueEvent ve = vedb.getVenueEventById(venueEventId);
            Seat s = sdb.getSeatFromSeatNo(seatNo);
            User u = udb.getUserByEmail(userEmail);

            Ticket ticket = new Ticket();
            ticket.venueEvent = ve;
            ticket.startDate = startDate;
            ticket.endDate = endDate;
            ticket.user = u;
            ticket.seat = s;
            ticket.ticket_ID = num.ToString();

            //DELETE THIS
            Console.WriteLine(endDate);
            Console.WriteLine(startDate);

            return ticket;
        }

        public List<Ticket> getAllTicketsBySeatNo(string seatNo) {
            TicketDB tdb = new TicketDB(Configuration);
            List<Ticket> tickets = tdb.getAllTicketsBySeatNo(seatNo);
            return tickets;
        }

        public bool addTicketToDb(Ticket ticket, int ticketCount) {
            TicketDAO tdb = new TicketDB(Configuration);
            bool res = tdb.addTicketToDB(ticket, ticketCount);
            return res;
        }

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
        public Boolean validateTicket(List<Ticket> existingTickets, Ticket validationTicket)
        {
            Boolean isDateOverlapped = checkDateOverlap(validationTicket.startDate, validationTicket.endDate, validationTicket.venueEvent.startDate, validationTicket.venueEvent.endDate);
            Boolean found = false;
            if (!isDateOverlapped)
            {
                
                for (int i=0; i<existingTickets.Count && !found; i++)
                {
                    if (checkSeatDateOverlap(validationTicket.startDate, validationTicket.endDate, existingTickets[i].startDate, existingTickets[i].endDate))
                    {
                        found = true;
                    }
                }
            }
            return (!isDateOverlapped && !found);

        }
        /// <summary>
        /// Checks if the date of the ticket matches the date of the event
        /// </summary>
        /// <param name="desiredStartDate">Start date of the possible ticket</param>
        /// <param name="desiredEndDate">End date of the possible ticket</param>
        /// <param name="eventStartDate">Event start date</param>
        /// <param name="eventEndDate">Event end date</param>
        /// <returns>False if there is no overlap</returns>
        private Boolean checkDateOverlap(DateTime desiredStartDate, DateTime desiredEndDate, DateTime eventStartDate, DateTime eventEndDate)
        {
            Boolean res = true;

            if (desiredStartDate >= eventStartDate && desiredEndDate <= eventEndDate)
            {
                res = false;
            }
            Console.WriteLine("Check Event date overlap res: " + res);
            return res;
        }

        /// <summary>
        /// Checks if a seat is available in the chosen time of the event
        /// </summary>
        /// <param name="desiredStartDate">Start date of the possible ticket</param>
        /// <param name="desiredEndDate">End date of the possible ticket</param>
        /// <param name="existingStartDate">Found seat date start</param>
        /// <param name="existingEndDate">Found seat date e</param>
        /// <returns>False if there is no overlap</returns>
        private Boolean checkSeatDateOverlap(DateTime desiredStartDate, DateTime desiredEndDate, DateTime existingStartDate, DateTime existingEndDate)

        {
            Boolean res = true;

            if (desiredStartDate < existingEndDate && existingStartDate > desiredEndDate ||
                desiredStartDate > existingEndDate && existingStartDate < desiredEndDate)
            {
                res = false;
            }
            Console.WriteLine("Check seat date overlap res: " + res);
            return res;
        }
    }
}

