using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketVenueSystem.Model;
using System.Configuration;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace TicketVenueDesktop.Service
{
    internal class VenueEventService : IVenueEventService
    {
        readonly IServiceConnection _connection;
        readonly string? _baseUrl = ConfigurationManager.AppSettings.Get("baseUrl");


        public VenueEventService()
        {
            _connection = new ServiceConnection(_baseUrl);
        }


        public async Task<Boolean> createVenueEvent(VenueEvent venueEvent)
        {
            Boolean ok = false;

            _connection.useUrl = _connection.baseUrl;
            _connection.useUrl += $"venueEvents/createVenueEvent";

            if (_connection != null)
            {
                try
                {
                    var json = JsonConvert.SerializeObject(venueEvent);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    String myContent = await content.ReadAsStringAsync();

                    await Console.Out.WriteLineAsync(myContent);


                    var response = await _connection.ServicePost(content);
                    bool wasResponse = (response != null);
                    if (wasResponse && response.IsSuccessStatusCode)
                    {
                        ok = true;
                    }
                }
                catch
                {
                    ok = false;
                }
            }
            return ok;
        }
    }
}
