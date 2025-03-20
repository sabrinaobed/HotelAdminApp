using HotelAdminApp.Services;
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

    }
}
