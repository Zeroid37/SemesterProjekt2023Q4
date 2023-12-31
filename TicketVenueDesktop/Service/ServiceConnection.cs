﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketVenueDesktop.Service
{
    public class ServiceConnection : IServiceConnection
    {
        public HttpClient httpClient { get; init; }
        public string baseUrl { get; init; }
        public string useUrl { get; set; }

        public ServiceConnection(String inputUrl) 
        { 
            httpClient = new HttpClient();
            baseUrl = inputUrl;
            useUrl = baseUrl;
        }

        /// <summary>
        /// GET for connecting to API Endpoints
        /// </summary>
        /// <returns>Response message</returns>
        public async Task<HttpResponseMessage> ServiceGet()
        {
            HttpResponseMessage response = null;

            if(useUrl != null)
            {
                response = await httpClient.GetAsync(useUrl);

            }
            return response;
        }

        /// <summary>
        /// POST method to hit API Endpoints with Json content
        /// </summary>
        /// <param name="jsonPost"></param>
        /// <returns>Response message</returns>
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
