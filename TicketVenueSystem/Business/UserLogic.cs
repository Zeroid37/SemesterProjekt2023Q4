using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketVenueSystem.DB;
using TicketVenueSystem.Model;

namespace TicketVenueSystem.Business
{
    public class UserLogic
    {
        private IConfiguration Configuration;
        private String? connectionString;

        public UserLogic(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionString = Configuration.GetConnectionString("DefaultConnection");
        }

        public Boolean addUserToDB(User user)
        {
            UserDAO udb = new UserDB(Configuration);

            Boolean res = udb.addUserToDB(user);
            return res;
        }

        public void setAspNetIdByEmail(string email, string userId)
        {
            UserDAO udb = new UserDB(Configuration);

            udb.setAspNetIdByEmail(email, userId);
        }


        public User getUserByEmail(String email)
        {
            UserDAO udb = new UserDB(Configuration);
            User user = udb.getUserByEmail(email);

            return user;

        }
    }
}
