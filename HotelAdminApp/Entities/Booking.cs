using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAdminApp.Entities
{
    class Booking
    {
        public int BookingId { get; set; } //Primary Key
        public Room Room { get; set; } //Many bookings can belong to one room over time (Many to One)
        public Customer Customer { get; set; } //Many bookings can belong to one customer over time (Many to One)
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfGuests { get; set; } //Number of guests for this booking should not exceed the room capacity
        public Invoice Invoice { get; set; } //One booking will have one invoice (One to one relationship)

    }
}
