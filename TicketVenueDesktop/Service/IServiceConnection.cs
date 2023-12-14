using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketVenueDesktop.Service
{
    public interface IServiceConnection
    {
        public string baseUrl { get; init; }
        public string useUrl { get; set; }

        Task<HttpResponseMessage> ServiceGet();
        Task<HttpResponseMessage> ServicePost(StringContent jsonPost);
    }
}
