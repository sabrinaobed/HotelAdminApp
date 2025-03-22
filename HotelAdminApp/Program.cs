using HotelAdminApp.Data;
using HotelAdminApp.Menu;
using HotelAdminApp.Controllers;    
using HotelAdminApp.Services;

namespace HotelAdminApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Initialize the database context
            using (var dbContext = DataInitializer.Build())
            {
                //Seed data if required
                DataInitializer.SeedData(dbContext);

                //Debug - Show counts to verify seeding worked
                Console.WriteLine("Room count: " + dbContext.Rooms.Count());
                Console.WriteLine("Customer count: " + dbContext.Customers.Count());
                Console.WriteLine("Booking count: " + dbContext.Bookings.Count());
                Console.WriteLine("Invoice count: " + dbContext.Invoices.Count());

                //Initialize services
                RoomService roomService = new RoomService(dbContext);
                CustomerService customerService = new CustomerService(dbContext);
                BookingService bookingService = new BookingService(dbContext);
                InvoiceService invoiceService = new InvoiceService(dbContext);


                // Initialize controllers with their respective services
                RoomController roomController = new RoomController(roomService);
                CustomerController customerController = new CustomerController(customerService);
                BookingController bookingController = new BookingController(bookingService);
                InvoiceController invoiceController = new InvoiceController(invoiceService);

                //Start the main menu
               var mainMenu = new MainMenu(roomController,customerController,bookingController,invoiceController);
                mainMenu.ShowMainMenu();


                Console.ReadLine();
            }
        }
    }
}
