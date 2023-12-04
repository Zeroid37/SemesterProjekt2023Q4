using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TicketVenueSystem.Model {
    public abstract class Person {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public Address address { get; set; }
        public string phoneNo { get; set; }
        public string email { get; set; }
        public Boolean isAdmin { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string type { get; set; }


        public Person(String type) 
        {
            this.type = type;
        }

        public Person(string firstName, string lastName, Address address, string phoneNo, string email, bool isAdmin, DateTime dateOfBirth, string type) {
            this.firstName = firstName;
            this.lastName = lastName;
            this.address = address;
            this.phoneNo = phoneNo;
            this.email = email;
            this.isAdmin = isAdmin;
            this.dateOfBirth = dateOfBirth;
            this.type = type;
        }
    }
}
