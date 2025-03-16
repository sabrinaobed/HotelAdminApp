using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAdminApp.Entities
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; } //Primary Key
       
        
        public int RoomId { get; set; }
        [ForeignKey("RoomId")]//Many bookings can belong to one room over time (Many to One)
        public Room Room { get; set; } = null!;




        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; } = null!;//Many bookings can belong to one customer over time (Many to One)
        
        
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfGuests { get; set; } //Number of guests for this booking should not exceed the room capacity
        public Invoice Invoice { get; set; } = null!;//One booking will have one invoice (One to one relationship)

    }
}
