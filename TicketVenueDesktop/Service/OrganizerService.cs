﻿using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TicketVenueSystem.Model;

namespace TicketVenueDesktop.Service
{
    public class OrganizerService : IOrganizerService
    {
        readonly IServiceConnection _connection;
        readonly string? _baseUrl = ConfigurationManager.AppSettings.Get("baseUrl");


        public OrganizerService()
        {
            _connection = new ServiceConnection(_baseUrl);
        }

        /// <summary>
        /// Get an event organizer by their OrganizerID
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns>EventOrganizer Object</returns>
        public async Task<EventOrganizer> getEventOrganizerByID(string orgId)
        {
            _connection.useUrl = _connection.baseUrl;
            _connection.useUrl += $"person/EventOrganizer/{orgId}";

            EventOrganizer? eOrg = new EventOrganizer();

            if (_connection != null)
            {
                try
                {
                    var response = await _connection.ServiceGet();
                    if (response != null && response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        eOrg = JsonConvert.DeserializeObject<EventOrganizer>(content);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return eOrg;
        }

        /// <summary>
        /// Get a list of all event organizers currently in the database.
        /// </summary>
        /// <returns>List of EventOrganizer objects</returns>
        public async Task<List<EventOrganizer>> getEventOrganizers()
        {
            _connection.useUrl = _connection.baseUrl;
            _connection.useUrl += "person/EventOrganizers";

            List<EventOrganizer>? eList = new List<EventOrganizer>();

            if(_connection != null)
            {
                try
                {
                    var response = await _connection.ServiceGet();
                    if (response != null && response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        eList = JsonConvert.DeserializeObject<List<EventOrganizer>>(content);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return eList;
        }
    }
}
