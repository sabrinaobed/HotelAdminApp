using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAdminApp.Entities
{
   public class Customer


    { [Key]
        public int CustomerId { get; set; } //Primary Key

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(255)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        
        [StringLength(15)]
        [Phone]
        public string? PhoneNumber { get; set; }
        
        
        
        
        public List<Booking> Bookings { get; set; } = new List<Booking>(); //One customer can have multiple bookings over time (One to Many relationship)
    }
}
