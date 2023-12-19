using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketVenueDesktop.Service;
using TicketVenueSystem.Business;
using TicketVenueSystem.Model;

namespace TicketVenueDesktop.BusinessLogic
{
    public class VenueEventLogic
    {
        private readonly IVenueEventService _venueEventService;

        public VenueEventLogic()
        {
            _venueEventService = new VenueEventService();
        }

        /// <summary>
        /// Create venue event, calls our service layer.
        /// </summary>
        /// <param name="venueEvent"></param>
        /// <returns>Boolean true or false</returns>
        public async Task<Boolean> createVenueEvent(VenueEvent venueEvent)
        {
            Boolean ok = false;
            try
            {
                ok = await _venueEventService.createVenueEvent(venueEvent);
            }
            catch
            {
                ok = false;
            }

            return ok;
        }


    }
}
