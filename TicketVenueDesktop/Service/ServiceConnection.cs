using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketVenueDesktop.Service
{
    public class ServiceConnection : IServiceConnection
    {
        public HttpClient httpClient { get; set; }
        public string baseUrl { get; set; }
        public string useUrl { get; set; }

        public ServiceConnection(String inputUrl) 
        { 
            httpClient = new HttpClient();
            baseUrl = inputUrl;
            useUrl = baseUrl;
        }

        public async Task<HttpResponseMessage> ServiceGet()
        {
            HttpResponseMessage response = null;

            if(useUrl != null)
            {
                response = await httpClient.GetAsync(useUrl);

            }
            return response;
        }

        public async Task<HttpResponseMessage> ServicePost(StringContent jsonPost)
        {
            HttpResponseMessage response = null;

            if (useUrl != null)
            {
                response = await httpClient.PostAsync(useUrl, jsonPost);

            }
            return response;
        }
    }
}
