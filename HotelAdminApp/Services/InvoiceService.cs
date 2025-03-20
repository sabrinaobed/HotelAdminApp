using HotelAdminApp.Contexts;
using HotelAdminApp.Entities;
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
    }
}
