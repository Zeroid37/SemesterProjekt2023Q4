﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketVenueSystem.Model;

namespace TicketVenueSystem.DB
{
    internal class UserDB : UserDAO
    {
        public bool addUserToDB(SqlConnection con, User user)
        {
            throw new NotImplementedException();
        }

        public User getUserByEmail(SqlConnection con, string email)
        {
            throw new NotImplementedException();
        }
    }
}
