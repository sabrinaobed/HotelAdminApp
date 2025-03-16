using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAdminApp.Entities
{
   public  class Invoice
    {
        [Key]   
        public int InvoiceId { get; set; } //Primary Key
        
       
        public int BookingId { get; set; }
        [ForeignKey("BookingId")]
        public Booking Booking { get; set; } = null!;//One invoice will be with one booking (One to One relationship)


        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal TotalAmount { get; set; }


        [Required]
        public bool IsPaid { get; set; }

       
        
        [Required]
        public DateTime DueDate { get; set; }
    }
}
