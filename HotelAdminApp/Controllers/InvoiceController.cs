using HotelAdminApp.Services;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
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
                return;
            }
            Console.WriteLine("\nList of Invoices: \n");
            foreach( var invoice in invoices)
            {
                Console.WriteLine($"Invoice ID: {invoice.InvoiceId},Booking ID: {invoice.BookingId}, Amount: {invoice.TotalAmount}, Paid: {invoice.IsPaid}, Due: {invoice.DueDate}");
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
    }
}
