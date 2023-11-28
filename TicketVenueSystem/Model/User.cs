using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketVenueSystem.Model
{
    public class User : Person
    {
        public string userId { get; set; }

        public User() : base("u") { }

        public User(string firstName, string lastName, Address address, string phoneNo, string email, string password, bool isAdmin, DateTime dateOfBirth) : base(firstName, lastName, address, phoneNo, email, password, isAdmin, dateOfBirth, "u") { }

        public User(string userId, string firstName, string lastName, Address address, string phoneNo, string email, string password, bool isAdmin, DateTime dateOfBirth) : base(firstName, lastName, address, phoneNo, email, password, isAdmin, dateOfBirth, "u") {
            this.userId = userId;
        }
    }
}
