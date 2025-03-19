using HotelAdminApp.Contexts;
using HotelAdminApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAdminApp.Services
{
   public  class CustomerService
    {
        //showing reference to database context
        private readonly ApplicationDbContext _dbContext; //_dbContext is a class variable

        //this constructor intializes databse context via DI and ensure we can interact with database without re-intializing it.
        public CustomerService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext; //_dbContext is a class variable ans dbContext is a parameter
        }

        //-------------------CURD OPERATIONS------------------------

                                  //Get all Customers
         
        public List<Customer> GetAllCustomers()
        {
            return _dbContext.Customers.ToList();
        }

   


        
                           //Get a specific customer by its ID
     public Customer? GetCustomerById(int id)
        {
            return _dbContext.Customers.Find(id);//searches  a customer by its PK, if object found its fine other ,? represents its null otherwise.
        }













    }



}
