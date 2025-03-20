using HotelAdminApp.Entities;
using HotelAdminApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAdminApp.Controllers
{
    public class BookingController
    {
        private readonly BookingService _bookingService;

        //this constructor intializes  Booking service
        public BookingController(BookingService bookingService)
        {
            _bookingService = bookingService;
        }



        //Get all bookings
        public void GetAllBookings()
        {
            Console.Clear();
            var bookings = _bookingService.GetAllBookings();

            if(bookings.Count == 0)
            {
                Console.WriteLine("No bookings found");
                return;
            }

            Console.WriteLine("\n List of Bookings:  \n");
            foreach(var booking in bookings)
            {
                Console.WriteLine($"Booking ID: {booking.BookingId}, RoomNumber: {booking.Room.RoomNumber}, CustomerName: {booking.Customer.Name}, StartDate: {booking.StartDate}, EndDate: {booking.EndDate} ");
            }
        }







        //Get booking by ID
        public void GetBookingById(int id)
        {
            Console.Write("Enter Booking ID: ");
            if(int.TryParse(Console.ReadLine(), out int bookingId))
            {
                var booking = _bookingService.GetBookingById(bookingId);
                if(booking != null)
                {
                    Console.WriteLine($"Booking ID: {booking.BookingId}, RoomNumber: {booking.Room.RoomNumber}, CustomerName: {booking.Customer.Name}, StartDate: {booking.StartDate}, EndDate: {booking.EndDate} ");

                }
                else
                {
                    Console.WriteLine("Booking not found.");
                }
            }
            else
            {
                Console.WriteLine("Inavlid ID. ");
            }
        }






        //Add a new booking
        public void AddBooking()
        {
            Console.Clear();
            Console.Write("Enter Customer ID: ");
            if(!int.TryParse(Console.ReadLine(), out int customerId))
            {
                Console.WriteLine("Invalid Customer ID. ");
                return;
            }



            Console.Write("Enter Room ID:  ");
            if (!int.TryParse(Console.ReadLine(), out int roomId))
            {
                Console.WriteLine("Invalid Room ID. ");
                return;
            }

            Console.Write("Enter Start Date(YYYY-MM-DD): ");
            if(!DateTime.TryParse(Console.ReadLine(),out DateTime startDate) || startDate < DateTime.Today)
            {
                Console.WriteLine("Invalid start date.");
                return;
            }


            Console.WriteLine("Enter End Date(YYYY-MM-DD): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate) || endDate <= startDate)
            {
                Console.WriteLine("Invalid end date.");
                return;
            }


            Console.WriteLine("Enter number of Guests: ");
            if(!int .TryParse(Console.ReadLine(),out int numberOfGuests) || numberOfGuests <= 0)
            {
                Console.WriteLine("invalid number of guests");
                return;
            }

            var booking = new Booking
            {
                CustomerId = customerId,
                RoomId = roomId,
                StartDate = startDate,
                EndDate = endDate,
                NumberOfGuests = numberOfGuests
            };

            _bookingService.AddBooking(booking);
            Console.WriteLine("Booking added successfully.");


        }
    }
}
