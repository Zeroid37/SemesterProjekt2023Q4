using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketVenueSystem.Model;

namespace TicketVenueDesktop.Service
{
    public interface IVenueEventService
    {
        Task<Boolean> createVenueEvent(VenueEvent venueEvent);
    }
}
