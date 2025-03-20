using HotelAdminApp.Contexts;
using HotelAdminApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAdminApp.Services
{
    public class InvoiceService
    {
        //showing the reference to database context
        private readonly ApplicationDbContext _dbContext; //_dbContext is a class variable

        //This constructor intialized database context via DI and ensure we can interact with database without re-intializing it.
        public InvoiceService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext; //_dbContext is a class variable and dbContext is a parameter
        }



        //-------------------CURD OPERATIONS------------------------

                                       //Get all Invoices
     public List<Invoice> GetAllInvoices()
        {
            return _dbContext.Invoices.ToList();
        }


                                 //Get a specific invoice by its ID  

        public Invoice? GetInvoiceById(int id)
        {
            return _dbContext.Invoices
                .Include(i => i.Booking) //include booking related to the invoice
                .ThenInclude(b => b.Customer)// then include cutomer related to the booking
                .FirstOrDefault(i => i.InvoiceId == id);//searches  a invoice by its PK, if object found its fine other ,? represents its null otherwise.
        }


                                  //Mark an invoice as paid /Update
        public void MarkInvoicePaid(int invoiceId)
        {
            var invoice = _dbContext.Invoices.Find(invoiceId);
            if(invoice == null)
            {
                throw new KeyNotFoundException($"Invoice with ID {invoiceId} not found.");
            }

            invoice.IsPaid = true;
            _dbContext.Invoices.Update(invoice);
            _dbContext.SaveChanges();

        }
    }
}
