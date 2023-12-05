using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketVenueSystem.Model {
    public class EventOrganizer : Person {
        public string organizerId { get; set; }

        public EventOrganizer() : base("e") { }
        public EventOrganizer(string firstName, string lastName, Address address, string phoneNo, string email, bool isAdmin, DateTime dateOfBirth) : base(firstName, lastName, address, phoneNo, email, isAdmin, dateOfBirth, "e") { }
        
        public EventOrganizer(string organizerId, string firstName, string lastName, Address address, string phoneNo, string email, bool isAdmin, DateTime dateOfBirth) : base(firstName, lastName, address, phoneNo, email, isAdmin, dateOfBirth, "e") {
            this.organizerId = organizerId;
        }
    }
}
