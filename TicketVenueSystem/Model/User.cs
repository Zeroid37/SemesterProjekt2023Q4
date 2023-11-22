using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketVenueSystem.Model
{
    public class User
    {
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phoneNo { get; set; }
        public DateTime dateOfBirth { get; set; }


        public User() { }

        public User(string id, string firstName, string lastName, string email, string phoneNo, DateTime dateOfBirth)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.phoneNo = phoneNo;
            this.dateOfBirth = dateOfBirth;
        }
    }
}
