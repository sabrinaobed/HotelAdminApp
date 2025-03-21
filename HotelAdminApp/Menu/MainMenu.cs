using HotelAdminApp.Controllers;
using HotelAdminApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAdminApp.Menu
{
    public class MainMenu
    {

        //used for DI and ensures all controllers have access to services for handling business logic

        private readonly RoomController _roomController;
        private readonly CustomerController _customerController;
        private readonly BookingController _bookingController;
        private readonly InvoiceController _invoiceController;



        //Constructor to initialize the controllers via DI
        public MainMenu(RoomController roomController, CustomerController customerController, BookingController bookingController, InvoiceController invoiceController)
        {
            _roomController = roomController;
            _customerController = customerController;
            _bookingController = bookingController;
            _invoiceController = invoiceController;
        }    
        

        
        //This method is to show main menu and runs in loop until user chooses to exit.
        public void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("---- Welcome to Hotel Admin App ---- \n");
                Console.WriteLine("1. Manage Rooms");
                Console.WriteLine("2. Manage Customers");
                Console.WriteLine("3. Manage Bookings");
                Console.WriteLine("4. Manage Invoices");
                Console.WriteLine("0. Exit");

                Console.Write("Select an option to proceed: \n");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RoomMenu();
                        break;
                    case "2":
                        CustomerMenu();
                        break;
                    case "3":
                        BookingMenu();
                        break;
                    case "4":
                          InvoiceMenu();
                        break;
                    case "0":
                        Console.WriteLine("Exiting the app...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice!Choose the correct option...");
                        break;
                }
            }
        }
        
            

        //------ SUB MENUS------
        //Sub menu for Room Management

        private void RoomMenu()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("---- Room Management Menu----\n");
                Console.WriteLine("1. Show All Rooms");
                Console.WriteLine("2. Add a Room");
                Console.WriteLine("3. Update a Room");
                Console.WriteLine("4. Delete a Room");
                Console.WriteLine("0. Back to Main Menu");

                Console.WriteLine("Select an option to proceed: \n");

                string choice = Console.ReadLine();

                switch(choice)
                {
                    case "1":
                        _roomController.GetAllRooms();
                        break;
                    case "2":
                        _roomController.AddRoom();
                        break;
                    case "3":
                        _roomController.UpdateRoom();
                        break;
                    case "4":
                        _roomController.DeleteRoom();
                        break;
                    case "0":
                        Console.WriteLine("Back to Main Menu");
                        return;
                    default:
                        Console.WriteLine("Invalid choice!Choose the correct option...");
                        break;
                }
            }

        }


        //Sub menu for Customer Management
        private void CustomerMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("---- Customer Management Menu ----\n");
                Console.WriteLine("1. Show All Customers");
                Console.WriteLine("2. Add a Customer");
                Console.WriteLine("3. Update a Customer");
                Console.WriteLine("4. Delete a Customer");
                Console.WriteLine("0. Back to Main Menu");

                Console.WriteLine("Select an option to proceed: \n");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        _customerController.GetAllCustomers();
                        break;
                    case "2":
                        _customerController.AddCustomer();
                        break;
                    case "3":
                        _customerController.UpdateCustomer();
                        break;
                    case "4":
                           _customerController.DeleteCustomer();
                        break;
                    case "0":
                        Console.WriteLine("Back to Main Menu");
                        return;
                    default:
                        Console.WriteLine("Invalid option!Please try again with the correct option...");
                        break;
                }

            }
        }


            //Sub menu for Bookings Management
        private void BookingMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("---- Booking Management Menu ----\n");
                Console.WriteLine("1. Show All Booking");
                Console.WriteLine("2. Add a Booking");
                Console.WriteLine("3. Update a Booking");
                Console.WriteLine("4. Delete a Booking");
                Console.WriteLine("0. Back to Main Menu");

                Console.WriteLine("Select an option to proceed: \n");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    case "0":
                        Console.WriteLine("Back to Main Menu");
                        return;
                    default:
                        Console.WriteLine("Invalid option!Please try again with the correct option...");
                        break;
                }

            }

        }


        

        //Sub menu for Invoice Management

        private void InvoiceMenu()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("---- Invoice Management Menu ----\n");
                Console.WriteLine("1. Show All Invoices");
                Console.WriteLine("2. Update Invoice as Paid");
                Console.WriteLine("0. Back to Main Menu");

                Console.WriteLine("Select an option to proceed: ");

                string choice = Console.ReadLine(); 

                switch(choice)
                {
                    case "1":
                        break;
                    case "2":
                        break;
                    case "0":
                        Console.WriteLine("Back to Main Menu.");
                        return;
                    default:
                        Console.WriteLine("Invalid choice! Please try again with the right option...");
                        break;
                }
            }

        }



    }
}
