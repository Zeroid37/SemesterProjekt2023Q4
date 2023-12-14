using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketVenueSystem.Model;
using System.Configuration;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace TicketVenueDesktop.Service
{
    public class HallService : IHallService
    {
        readonly IServiceConnection _connection;
        readonly string? _baseUrl = ConfigurationManager.AppSettings.Get("baseUrl");


        public HallService()
        {
            _connection = new ServiceConnection(_baseUrl);
        }

        public async Task<Hall> GetHallAndSeats(string id)
        {
            _connection.useUrl = _connection.baseUrl;
            _connection.useUrl += $"hall/halls/{id}";

            Hall? hall = new Hall();

            if (_connection != null)
            {
                try
                {
                    var response = await _connection.ServiceGet();
                    if (response != null && response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        hall = JsonConvert.DeserializeObject<Hall>(content);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return hall;
        }
    }
}
