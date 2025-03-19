using HotelAdminApp.Contexts;
using HotelAdminApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAdminApp.Services
{
    public class CustomerService
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






        //Add a new customer with all valid attributes


        //methods to validate email nd phone nuumber for a customer
        private bool IsValidEmail(string email)
        {
            return email.Contains("@") && email.Contains(".");
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            return phoneNumber.All(char.IsDigit) && phoneNumber.Length >= 8;
        }


        public void AddCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            //validate customer name
            if (string.IsNullOrWhiteSpace(customer.Name))
            {
                throw new ArgumentException("Customer name cannot be empty");
            }

            //validate customer email 
            if (!IsValidEmail(customer.Email))
            {
                throw new ArgumentException("Invalid Email Address and format");
            }
            if (_dbContext.Customers.Any(c => c.Email == customer.Email))//if email already exists
            {
                throw new ArgumentException("Email already exists");
            }


            //validate customer phone number
            if (!string.IsNullOrWhiteSpace(customer.PhoneNumber) && !IsValidPhoneNumber(customer.PhoneNumber))
            {
                throw new ArgumentException("Invalid Phone Number and format");
            }

            _dbContext.Customers.Add(customer); //Add customer to DbSet
            _dbContext.SaveChanges(); //Save changes to database
        }





        //Update an existing customer with all valid attributes

        public void UpdateCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer), "Customer cannot be null");
            }

            var existingCustomer = _dbContext.Customers.Find(customer.CustomerId);
            if (existingCustomer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {customer.CustomerId} not found.");
            }

            existingCustomer.Name = customer.Name;
            existingCustomer.Email = customer.Email;
            existingCustomer.PhoneNumber = customer.PhoneNumber;

            _dbContext.Customers.Update(existingCustomer);
            _dbContext.SaveChanges();

        }










    }



}
