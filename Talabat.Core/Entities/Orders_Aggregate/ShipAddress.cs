using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Orders_Aggregate
{
    public class ShipAddress
    {
        public ShipAddress()
        {
            
        }
        public ShipAddress(string firstName, string lastName, string street, string country, string city)
        {
            FirstName = firstName;
            LastName = lastName;
            Street = street;
            Country = country;
            City = city;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}