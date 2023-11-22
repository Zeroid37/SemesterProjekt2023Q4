using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketVenueSystem.Model;

namespace TicketVenueSystem.DB
{
    internal interface UserDAO
    {
        public Boolean addUserToDB(User user);
        public User getUserByEmail(String email);
    }
}
