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
                
            }
            else
            {
                Console.WriteLine("\n List of Bookings:  \n");
                foreach (var booking in bookings)
                {
                    Console.WriteLine($"Booking ID: {booking.BookingId}, RoomNumber: {booking.Room.RoomNumber}, CustomerName: {booking.Customer.Name}, StartDate: {booking.StartDate}, EndDate: {booking.EndDate} ");
                }

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
            Console.WriteLine("\nCurrent Booking List: ");
            var allBookingsBeforeAddition = _bookingService.GetAllBookings();
            foreach(var bookings in allBookingsBeforeAddition)
            {
                Console.WriteLine($"Booking ID: {bookings.BookingId}, RoomNumber: {bookings.Room.RoomNumber}, CustomerName: {bookings.Customer.Name}, StartDate: {bookings.StartDate}, EndDate: {bookings.EndDate} ");

            }



            //ADD DETAILS FOR NEW BOOKING

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


            //adding booking using booking service
           Booking newBooking = new Booking
            {
                CustomerId = customerId,
                RoomId = roomId,
                StartDate = startDate,
                EndDate = endDate,
                NumberOfGuests = numberOfGuests
            };

            _bookingService.AddBooking(newBooking);
            Console.WriteLine("Booking added successfully.");

            //Fetch all bookings again
            var allBookings = _bookingService.GetAllBookings();

            //Show updated bookings
            Console.WriteLine("\nUpdated Booking List: ");
            foreach (var booking in allBookings)
            {
                if(booking.RoomId == newBooking.RoomId &&
                    booking.CustomerId == newBooking.CustomerId &&
                    booking.StartDate == newBooking.StartDate &&
                    booking.EndDate == newBooking.EndDate &&
                    booking.NumberOfGuests == newBooking.NumberOfGuests)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.WriteLine($"Booking ID: {booking.BookingId}, RoomNumber: {booking.Room.RoomNumber}, CustomerName: {booking.Customer.Name}, StartDate: {booking.StartDate}, EndDate: {booking.EndDate} ");

                //reset color
                Console.ResetColor();   



            }


        }





        //Update a booking
        public void UpdateBooking()
        {
            Console.WriteLine("Enter Booking ID: ");
            if(!int.TryParse(Console.ReadLine(), out int bookingId))
            {
                var booking = _bookingService.GetBookingById(bookingId);
                if(booking == null)
                {
                    Console.WriteLine("Booking not found.");
                    return;
                }


                Console.WriteLine($"Updating Booking ID: {booking.BookingId}");

                Console.Write("Enter new Start Date(YYYY-MM-DD): ");
                string startInput = Console.ReadLine();
                if(!string.IsNullOrWhiteSpace(startInput) && DateTime.TryParse(startInput,out DateTime newStartDate) && newStartDate >= DateTime.Today)
                {
                    booking.StartDate = newStartDate;
                }



                Console.Write("Enter new End Date(YYYY-MM-DD): ");
                string endInput = Console.ReadLine();
                if(!string.IsNullOrWhiteSpace(endInput) && DateTime.TryParse(endInput,out DateTime newEndDate) && newEndDate > booking.StartDate)
                {
                    booking.EndDate = newEndDate;
                }


                Console.WriteLine("Enter new number of guests: ");
                string guestsInput = Console.ReadLine();
                if (!int.TryParse(guestsInput, out int newGuests) && newGuests > 0)
                {
                    booking.NumberOfGuests = newGuests;
                }

                _bookingService.UpdateBooking(booking);
                Console.WriteLine("Booking updated succesfully!");
            }
            else
            {
                Console.WriteLine("Invalid Input");
            }

            
        }





        //Delete a booking
        public void DeleteBooking()
        {
            Console.WriteLine("Enter booking ID: ");
            if(int.TryParse(Console.ReadLine(),out int bookingId))
            {
                var booking = _bookingService.GetBookingById(bookingId);
                if (booking == null)
                {
                    Console.WriteLine("Booking not found.");
                    return;
                }

                Console.WriteLine($"Are you sure you want to cancel Booking with ID: {booking.BookingId}? (yes/no)");
                string confirmation = Console.ReadLine()?.ToLower();
                if (confirmation == "yes")
                {
                    _bookingService.DeleteBooking(bookingId);
                    Console.WriteLine("Booking cancelled successfully.");
                }
                else
                {
                    Console.WriteLine("Booking not cancelled.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input");
            }
        }


    }
}
