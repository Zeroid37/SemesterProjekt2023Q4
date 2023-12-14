﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketVenueSystem.Model;

namespace TicketVenueDesktop.Service
{
    public interface IHallService
    {
        Task<Hall> GetHallAndSeats(String id);
    }
}
