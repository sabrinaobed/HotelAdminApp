using HotelAdminApp.Services;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAdminApp.Controllers
{
    public class InvoiceController
    {
        private readonly InvoiceService _invoiceService;

        //this constructor intiliazes Invoices service via DI
        public InvoiceController(InvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }


        //Get all invoices
        public void GetAllInvoices()
        {
            Console.Clear();
            var invoices = _invoiceService.GetAllInvoices();

            if(invoices.Count == 0)
            {
                Console.WriteLine("No invoices found");
                
            }
            else
            {
                foreach (var invoice in invoices)
                {
                    Console.WriteLine($"Invoice ID: {invoice.InvoiceId},Booking ID: {invoice.BookingId}, Amount: {invoice.TotalAmount}, Paid: {invoice.IsPaid}, Due: {invoice.DueDate}");
                }
            }
               
            
        }











        //Get invoice by ID
        public void GetInvoiceById(int id)
        {
            Console.WriteLine("Enter Invoice ID: ");
            if(int.TryParse(Console.ReadLine(),out int invoiceId))
            {
                var invoice = _invoiceService.GetInvoiceById(invoiceId);
                if(invoice != null)
                {
                    Console.WriteLine($"Invoice ID: {invoice.InvoiceId},Booking ID: {invoice.BookingId}, Amount: {invoice.TotalAmount}, Paid: {invoice.IsPaid}, Due: {invoice.DueDate}");

                }
                else
                {
                    Console.WriteLine("Invoice not found ");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID");
            }
        }








        //Mark an invoice as paid
        public void MarkInvoicePaid()
        {

            Console.Clear();
            Console.WriteLine("\n Mark Invoice as Paid: ");


            //show all invoices
            var allInvoices = _invoiceService.GetAllInvoices().ToList();
            if(allInvoices.Count == 0)
            {
                Console.WriteLine("There is no invoice in system.");
                return;
            }

            foreach (var invoice in allInvoices)
            {
                string paidStatus = invoice.IsPaid ? " PAID" : " UNPAID";
                Console.WriteLine($"Invoice ID: {invoice.InvoiceId}, Booking ID: {invoice.BookingId}, Total: {invoice.TotalAmount} SEK, Status: {paidStatus}");
            }



            //ask for invocie details:
            Console.WriteLine("Enter Invoice ID to mark as paid: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int invoiceId))

            {
                //Try to mark as paid
                try
                {
                    _invoiceService.MarkInvoicePaid(invoiceId);

                    Console.WriteLine("Invoice marked as paid successfully");
                }
                catch (KeyNotFoundException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID");
            }
          

        }






        //Cancels the overdue bookings
        public void CancelOverdueBookings()
        {
            Console.WriteLine("Checking for unpaid invoices older than 10 days...");
            try
            {
                _invoiceService.CancelBookingIfInvoiceOverDue();
                Console.WriteLine("Overdue bookings cancelled successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cancelling overdue bookings: {ex.Message}");
            }
        }
    }
}
