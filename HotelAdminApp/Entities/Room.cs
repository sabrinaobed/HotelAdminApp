using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAdminApp.Entities
{
    class Room
    {
        public int RoomId { get; set; }//Primary Key
        public string RoomNumber { get; set; } 
        public string RoomType { get; set; } // Single/ Double
        public int Capacity { get; set; } //Max number of guests allowed
        public decimal PricePerNight { get; set; } //Max number of guests allowed
        public List<Booking> Bookings { get; set; } // One room can have mutiple bookings over time (One to Many relationship)

    }
}
