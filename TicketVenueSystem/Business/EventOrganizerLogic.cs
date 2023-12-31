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
    public class EventOrganizerLogic
    {
        private IConfiguration Configuration;
        private String? connectionString;

        public EventOrganizerLogic(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionString = Configuration.GetConnectionString("DefaultConnection");
        }

        /// <summary>
        /// Method to get all event organizers
        /// </summary>
        /// <returns>List of EventOrganizer objects</returns>
        public List<EventOrganizer> getAllEventOrganizers()
        {
            UserDAO udb = new UserDB(Configuration);
            List<EventOrganizer> eList = udb.getAllEventOrganizers();

            return eList;
        }

        /// <summary>
        /// Method to get a single event organizer from their OrganizerId
        /// </summary>
        /// <param name="id"></param>
        /// <returns>EventOrganizer object</returns>
        public EventOrganizer getEventOrganizerById(String id)
        {
            UserDAO udb = new UserDB(Configuration);
            EventOrganizer eOrg = udb.getEventOrganizerByID(id);

            return eOrg;
        }

    }
}
