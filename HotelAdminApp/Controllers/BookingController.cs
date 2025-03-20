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
    }
}
