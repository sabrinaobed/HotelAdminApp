using HotelAdminApp.Services;
using HotelAdminApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAdminApp.Controllers
{
    public  class CustomerController //it handles user interactions for managing rooms and ensures data validation before callin RoomService
    {
        private readonly CustomerService _customerService;

        //this constructor intializes the customer service via DI
        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }


        //Get all Customers
        public void GetAllCustomers()
        {
            Console.Clear();
            var customers = _customerService.GetAllCustomers();

            if (customers.Count == 0)
            {
                Console.WriteLine("\nList of all Customers\n");
                foreach(var customer in customers)
                {
                    Console.WriteLine($" ID: {customer.CustomerId}, CustomerName: {customer.Name}, Email: {customer.Email}, PhoneNumber: {customer.PhoneNumber}");
                }
            }
        }






        //Get a customer by ID
        public void GetCustomerById()
        {
            Console.WriteLine("Enter Customer ID: ");
            if (int.TryParse(Console.ReadLine(), out int customerId))
            {
                var customer = _customerService.GetCustomerById(customerId);
                if(customer != null)
                {
                    Console.WriteLine($"ID: {customer.CustomerId},CustomerName: {customer.Name}, Email: {customer.Email}, Phone: {customer.PhoneNumber}");
                }
                else
                {
                    Console.WriteLine("Customer not found");
                }

            }
            else
            {
                Console.WriteLine("Invalid Customer ID. ");
            }
        }







        //Add a new customer
        public void AddCustomer()
        {
            Console.Clear();
            Console.WriteLine("Enter Customer Name: ");
            string name = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Customer name cannot be empty");
                return;
            }



            Console.WriteLine("Enter Customer Email: ");
            string email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                Console.WriteLine("Invalid email format.");
                return;
            }


            Console.WriteLine("Enter Customer Phone Number: ");
            string phoneNumber = Console.ReadLine();
            
            var customer = new Customer
            {
                Name = name,
                Email = email,
                PhoneNumber = phoneNumber
            };

            _customerService.AddCustomer(customer);
            Console.WriteLine("Customer added successfully");



        }




        //Update a customer
        public void UpdateCustomer()
        {
            Console.WriteLine("Enter Customer ID: ");
            if(int.TryParse(Console.ReadLine(), out int customerId))
            {
                var customer = _customerService.GetCustomerById(customerId);
                if (customer == null)
                {
                    Console.WriteLine("Customer not found");
                    return;
                }

                Console.WriteLine($"Updating Customer: {customer.Name}");

                Console.WriteLine("Enter new name:  ");
                string newName = Console.ReadLine();
                if(!string.IsNullOrWhiteSpace(newName))
                    customer.Name = newName;


                Console.WriteLine("Enter new email:  ");
                string newEmail = Console.ReadLine();
                if(!string.IsNullOrWhiteSpace(newEmail) && newEmail.Contains("@"))
                    customer.Email = newEmail;


                Console.WriteLine("Enter new phone number:  ");
                string newPhoneNumber = Console.ReadLine();
                if(!string.IsNullOrWhiteSpace(newPhoneNumber))
                    customer.PhoneNumber = newPhoneNumber;

                _customerService.UpdateCustomer(customer);
                Console.WriteLine("Customer updated successfully!");

            }
            else
            {
                Console.WriteLine("Invalid input");
            }
        }

    }
}
