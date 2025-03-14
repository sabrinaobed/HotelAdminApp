using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAdminApp.Entities
{
    class Invoice
    {
        public int InvoiceId { get; set; } //Primary Key
        public Booking Booking { get; set; }//One invoice will be with one booking (One to One relationship)
        public decimal TotalAmount { get; set; }
        public bool IsPaid { get; set; }
        public DateTime DueDate { get; set; }
    }
}
