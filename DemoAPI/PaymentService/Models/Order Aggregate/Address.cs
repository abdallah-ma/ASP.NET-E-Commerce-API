using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Models
{
    public class Address
    {

        public Address()
        {
            
        }

        public Address(string fName, string lName, string country, string city, string street)
        {
            FirstName = fName;
            LastName = lName;
            Country = country;
            City = city;
            Street = street;
        }

        public string  FirstName { get; set; }

        public string  LastName { get; set; }

        public string Country { get; set; }
        
        public string City { get; set; }
        
        public string Street { get; set; }

    }
}
