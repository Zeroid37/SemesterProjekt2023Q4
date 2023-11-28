﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketVenueSystem.Model {
    public class Address {
        public string street { get; set; }
        public string houseNo { get; set; }
        public string zip { get; set; }
        public string city { get; set; }

        public Address() { }
        public Address(string street, string houseNo, string zip, string city) {
            this.street = street;
            this.houseNo = houseNo;
            this.zip = zip;
            this.city = city;
        }
    }
}
