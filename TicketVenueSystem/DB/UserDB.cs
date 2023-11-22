using System;
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
        public bool addUserToDB(User user)
        {
            throw new NotImplementedException();
        }

        public User getUserByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
