using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketVenueDesktop.Service;
using TicketVenueSystem.Model;

namespace TicketVenueDesktop.BusinessLogic
{
    public class OrganizerLogic
    {
        private readonly IOrganizerService _organizerService;

        public OrganizerLogic()
        {
            _organizerService = new OrganizerService();
        }

        /// <summary>
        /// Get all event organizers
        /// </summary>
        /// <returns>List of Event Organizers</returns>
        public async Task<List<EventOrganizer>> getAllEventOrganizers()
        {
            List<EventOrganizer> eList;

            try
            {
                eList = await _organizerService.getEventOrganizers();
            }
            catch
            {
                eList = null;
            }

            return eList;
        }

        /// <summary>
        /// Get event organizer by their OrganizerId
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Event Organizer Object</returns>
        public async Task<EventOrganizer> getEventOrganizerById(String id)
        {
            EventOrganizer eOrg;

            try
            {
                eOrg = await _organizerService.getEventOrganizerByID(id);
            }
            catch
            {
                eOrg = null;
            }

            return eOrg;
        }
    }
}
