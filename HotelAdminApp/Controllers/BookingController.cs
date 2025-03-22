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
        private readonly CustomerService _customerService;
        private readonly RoomService _roomService;

        //this constructor intializes  Booking service
        public BookingController(BookingService bookingService, CustomerService customerService, RoomService roomService)
        {
            _bookingService = bookingService;
            _customerService = customerService;
            _roomService = roomService;
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
            Console.WriteLine("\nAdd a New Booking: ");

            //show all customers
            Console.WriteLine("\nList of all customers: ");
            var allCustomers = _customerService.GetAllCustomers();
            foreach(var c in allCustomers)
            {
                Console.WriteLine($"Customer ID: {c.CustomerId}, Name: {c.Name}, Email: {c.Email}, PhoneNumber: {c.PhoneNumber}");
            }

            //show all rooms
            Console.WriteLine("\nList of All Rooms(BOOKED rooms are highlighted in yellow):");
            var allRooms = _roomService.GetAllRooms();
            var bookedRoomIds = _bookingService.GetAllBookings().Select(b => b.RoomId).Distinct().ToList();
            foreach(var room in allRooms)
            {
                bool isBooked = bookedRoomIds.Contains(room.RoomId);
                if (isBooked)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }


                    Console.WriteLine($"ID: {room.RoomId}, RoomNumber: {room.RoomNumber}, RoomType: {room.RoomType}, Capacity: {room.Capacity}, Price: {room.PricePerNight}" +
                                (isBooked ? "    BOOKED" : ""));
                    Console.ResetColor();
                
            }



            //ADD DETAILS FOR NEW BOOKING

            Console.Write("\nEnter Customer ID: ");
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

            Console.Clear();
            GetAllBookings();


            Console.WriteLine("Enter Booking ID: ");
            if(!int.TryParse(Console.ReadLine(), out int bookingId))
            {
                var booking = _bookingService.GetBookingById(bookingId);
                if(booking == null)
                {
                    Console.WriteLine("Booking not found.");
                    return;
                }


                Console.WriteLine($"\nUpdating Booking with ID: {booking.BookingId}");



                Console.Write("Enter new Start Date(YYYY-MM-DD) (Press enter to continue with same): ");
                string startInput = Console.ReadLine();
                if(!string.IsNullOrWhiteSpace(startInput) && DateTime.TryParse(startInput,out DateTime newStartDate) && newStartDate >= DateTime.Today)
                {
                    booking.StartDate = newStartDate;
                }



                Console.Write("Enter new End Date(YYYY-MM-DD) (Press enter to continue with same): ");
                string endInput = Console.ReadLine();
                if(!string.IsNullOrWhiteSpace(endInput) && DateTime.TryParse(endInput,out DateTime newEndDate) && newEndDate > booking.StartDate)
                {
                    booking.EndDate = newEndDate;
                }


                Console.WriteLine("Enter new number of guests (Press enter to continue with same): ");
                string guestsInput = Console.ReadLine();
                if (!int.TryParse(guestsInput, out int newGuests) && newGuests > 0)
                {
                    booking.NumberOfGuests = newGuests;
                }




                _bookingService.UpdateBooking(booking);
                Console.WriteLine("\nBooking updated succesfully!");

                Console.WriteLine("\nUpdated Booking List: ");
                var updatedBookings = _bookingService.GetAllBookings();

                foreach(var b in updatedBookings)
                {
                    if (b.BookingId == booking.BookingId)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    
                    Console.WriteLine($"Booking ID: {b.BookingId}, RoomNumber: {b.Room.RoomNumber}, CustomerName: {b.Customer.Name}, StartDate: {b.StartDate}, EndDate: {b.EndDate} ");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.WriteLine("Invalid Input");
            }

            
        }





        //Delete a booking
        public void DeleteBooking()
        {
            Console.Clear();
            GetAllBookings(); // Show list before deletion

            Console.Write("\nEnter booking ID: ");
            if (int.TryParse(Console.ReadLine(), out int bookingId))
            {
                var booking = _bookingService.GetBookingById(bookingId);
                if (booking == null)
                {
                    Console.WriteLine("Booking not found.");
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n⚠️ This booking has an invoice. Deleting it will also delete the related invoice.");
                Console.ResetColor();

                Console.Write($"\nAre you sure you want to cancel Booking with ID: {booking.BookingId}? (yes/no): ");
                string confirmation = Console.ReadLine()?.ToLower();
                if (confirmation == "yes")
                {
                    _bookingService.DeleteBooking(bookingId);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("✅ Booking and related invoice deleted successfully.");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("❌ Booking not deleted.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input");
            }

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }



    }
}
