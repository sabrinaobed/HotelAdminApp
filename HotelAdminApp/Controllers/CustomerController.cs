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
                    Console.WriteLine("No customers found");
            }
            else
            {
                    foreach (var customer in customers)
                    {
                        Console.WriteLine($" ID: {customer.CustomerId},  CustomerName: {customer.Name},  Email: {customer.Email},  PhoneNumber: {customer.PhoneNumber}");
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
            Console.WriteLine("\n Current Customer List: ");
            var allCustomersBeforeAddition = _customerService.GetAllCustomers();
            foreach(var customer in allCustomersBeforeAddition)
            {
                Console.WriteLine($"ID: {customer.CustomerId},  CustomerName: {customer.Name},  Email: {customer.Email},  PhoneNumber: {customer.PhoneNumber}");
            }



            //ADD DETAILS FOR NEW CUSTOMER
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
            

            //Adding new customer using customer service
            Customer newCustomer = new Customer
            {
                Name = name,
                Email = email,
                PhoneNumber = phoneNumber
            };

            _customerService.AddCustomer(newCustomer);


            Console.WriteLine("Customer added successfully");

            //Fetch all rooms again
            var allCustomers = _customerService.GetAllCustomers();

            //Show updated List
            Console.WriteLine("\n Updated Customer List: ");

            foreach(var customer in allCustomers)
            {
              if(customer.Name == newCustomer.Name && customer.Email == newCustomer.Email && customer.PhoneNumber == newCustomer.PhoneNumber)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.WriteLine($"ID: {customer.CustomerId},  CustomerName: {customer.Name},  Email: {customer.Email},  PhoneNumber: {customer.PhoneNumber}");


                //reset color
                Console.ResetColor();
            }




        }




        //Update a customer
        public void UpdateCustomer()
        {
            Console.Clear();
            GetAllCustomers(); //Show customers before updating

            Console.Write("\nEnter Customer ID: ");
            if (int.TryParse(Console.ReadLine(), out int customerId))
            {
                var customer = _customerService.GetCustomerById(customerId);
                if (customer == null)
                {
                    Console.WriteLine("Customer not found.");
                    return;
                }

                Console.WriteLine($"\nUpdating Customer: {customer.Name}");

                // Ask for new name and update if provided
                Console.Write("Enter new name: ");
                string newName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newName))
                    customer.Name = newName;

                // Ask for new email and validate format before updating
                Console.Write("Enter new email: ");
                string newEmail = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newEmail) && newEmail.Contains("@"))
                    customer.Email = newEmail;

                // Ask for new phone number and update if provided
                Console.Write("Enter new phone number: ");
                string newPhoneNumber = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newPhoneNumber))
                    customer.PhoneNumber = newPhoneNumber;

                // Save updated customer to database
                _customerService.UpdateCustomer(customer);
                Console.WriteLine("\nCustomer updated successfully!");

                // Show updated customer list correctly
                Console.WriteLine("\nUpdated Customer List:");
                var updatedCustomers = _customerService.GetAllCustomers();

                foreach (var c in updatedCustomers)
                {
                    // Correct condition to apply color only to updated customer
                    if (c.CustomerId == customerId)
                    {
                        Console.ForegroundColor = ConsoleColor.Green; //Highlight updated customer
                    }

                    // Print each customer from `updatedCustomers` list instead of `customer`
                    Console.WriteLine($"ID: {c.CustomerId},  CustomerName: {c.Name},  Email: {c.Email},  PhoneNumber: {c.PhoneNumber}");

                    Console.ResetColor(); //Reset text color back to default
                }

               
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid Customer ID.");
            }
        }








        //Delete a customer
        public void DeleteCustomer()
        {

            Console.Clear();
            GetAllCustomers();



            Console.WriteLine("Enter Customer ID:  ");
            if(int.TryParse(Console.ReadLine(),out int customerId))
            {
                var customer = _customerService.GetCustomerById(customerId);
                if(customer == null)
                {
                    Console.WriteLine("Customer not found");
                    return;
                }

                Console.WriteLine($"Are you sure you want to delete {customer.Name}? (yes/no)");
                string confirmation = Console.ReadLine()?.ToLower();

                if (confirmation == "yes")
                {
                    try
                    {
                        _customerService.DeleteCustomer(customerId);
                        Console.WriteLine("\nRoom deleted successfully!");

                        //Show updated list
                        Console.WriteLine("\nUpdated Room List:");
                        GetAllCustomers();
                        Console.ReadLine();
                    }
                    catch (InvalidOperationException ex) //Catch the error instead of crashing
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"\n{ex.Message}"); // Show user-friendly error
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.WriteLine("\nDeletion canceled.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid Room ID.");
            }
        }

    }
                
}



