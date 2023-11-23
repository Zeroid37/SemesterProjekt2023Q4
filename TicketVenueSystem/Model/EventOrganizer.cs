using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketVenueSystem.Model {
    public class EventOrganizer : Person {
        public string organizerId { get; set; }

        public EventOrganizer(string firstName, string lastName, Address address, string phoneNo, string email, string password, bool isAdmin, DateTime dateOfBirth) : base(firstName, lastName, address, phoneNo, email, password, isAdmin, dateOfBirth, "e") { }
        
        public EventOrganizer(string organizerId, string firstName, string lastName, Address address, string phoneNo, string email, string password, bool isAdmin, DateTime dateOfBirth) : base(firstName, lastName, address, phoneNo, email, password, isAdmin, dateOfBirth, "e") {
            this.organizerId = organizerId;
        }
    }
}
