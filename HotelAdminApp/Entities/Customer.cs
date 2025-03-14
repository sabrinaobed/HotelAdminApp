using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAdminApp.Entities
{
    class Customer
    {
        public int CustomerId { get; set; } //Primary Key
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<Booking> Bookings { get; set; } //One customer can have multiple bookings over time (One to Many relationship)
    }
}
