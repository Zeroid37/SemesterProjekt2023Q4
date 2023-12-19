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

        /// <summary>
        /// Method to attempt to add a user to the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Boolean true or false</returns>
        public Boolean addUserToDB(User user)
        {
            UserDAO udb = new UserDB(Configuration);

            Boolean res = udb.addUserToDB(user);
            return res;
        }

        /// <summary>
        /// Method to set the AspNet ID to something based on the user's email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="userId"></param>
        public void setAspNetIdByEmail(string email, string userId)
        {
            UserDAO udb = new UserDB(Configuration);

            udb.setAspNetIdByEmail(email, userId);
        }

        /// <summary>
        /// Method to get a user object by their email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>User object</returns>
        public User getUserByEmail(String email)
        {
            UserDAO udb = new UserDB(Configuration);
            User user = udb.getUserByEmail(email);

            return user;

        }
    }
}
