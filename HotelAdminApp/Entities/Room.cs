using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAdminApp.Entities
{
   public class Room
    {

        [Key]
        public int RoomId { get; set; }//Primary Key

        
        [Required]
        [StringLength(10)]
        public string RoomNumber { get; set; } = null!; //Room Number


        [Required]
        [StringLength(50)]
        public string RoomType { get; set; } = null!;// Single/ Double

        [Required]
        public int Capacity { get; set; } //Max number of guests allowed

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal PricePerNight { get; set; } //Max number of guests allowed
        
        
     
        public int? ExtraBeds { get; set; } //Number of extra beds available in the room
        
        
        public List<Booking> Bookings { get; set; } = new List<Booking>();// One room can have mutiple bookings over time (One to Many relationship)

    }
}
