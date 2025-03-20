using HotelAdminApp.Contexts;
using HotelAdminApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAdminApp.Services
{
   public  class BookingService
   {
        private readonly ApplicationDbContext _dbContext; //_dbContext is a class variable


        //intialized ApplicationDBContext via DI
        public BookingService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext; //_dbContext is a class variable ans dbContext is a parameter
        }


        //-------------------CURD OPERATIONS------------------------

                                            //Get all Bookings
        public List<Booking> GetAllBookings()
        {
            return _dbContext.Bookings.Include(b => b.Customer).Include(b => b.Room).ToList(); //including customer and room as well in bookings
        }

                                               //Get a specific booking by its ID
        public Booking? GetBookingById(int id)
        {
            return _dbContext.Bookings.Include(b => b.Customer).Include(b => b.Room).FirstOrDefault(b => b.BookingId == id);//searches  a booking by its PK, if object found its fine other ,? represents its null otherwise.
        }



                                                //Add a new booking with all valid attributes


        public void AddBooking(Booking booking)
        {
            if(booking == null)
            {
                throw new ArgumentNullException(nameof(booking), "Booking cannot be null.");
            }
            //check if the customer exists
            var customerExists = _dbContext.Customers.Any(c => c.CustomerId == booking.CustomerId);
            if(!customerExists)
            {
                throw new ArgumentException($"Invalid Customer ID, customer does not exist");
            }
            //check if the room exists
            var roomExists = _dbContext.Rooms.Any(r => r.RoomId == booking.RoomId);
            if(!roomExists)
            {
                throw new ArgumentException($"Invalid Room ID, room does not exist");
            }
            //ensuring start date is today or in coming days
            if(booking.StartDate < DateTime.Today)
            {
                throw new ArgumentException("Cannot book a room for past dates");
            }
            //ensuring end date is after start date
            if(booking.EndDate <= booking.StartDate)
            {
                throw new ArgumentException("End date must be after the start date");
            }
            //Check if the room is available for the given dates
            bool roomBooked = _dbContext.Bookings.Any(
                b => b.RoomId == booking.RoomId &&
                booking.StartDate < b.EndDate &&
                booking.EndDate > b.StartDate
                );
            if (roomBooked)
            {
                throw new InvalidOperationException("Room is already booked for the given dates");
            }

            _dbContext.Bookings.Add(booking); //Add booking to DbSet
            _dbContext.SaveChanges(); //Save changes to database

        }



                                               //Update a booking with all valid attributes
         


        public void UpdateBooking(Booking booking)
        {
            if (booking == null)
            {
                throw new ArgumentNullException(nameof(booking), "Booking cannot be null.");
            }
            //check if the booking exists
            var existingBooking = _dbContext.Bookings.Find(booking.BookingId);
            if(existingBooking == null)
            {
                throw new KeyNotFoundException($"Booking with ID {booking.BookingId} not found.");
            }
            //check the startdate is today or in future
            if(booking.StartDate < DateTime.Today)
            {
                throw new ArgumentException("Cannot book a room for past dates");
            }
            //check the update booking may not cause conflict with previous bookings
            bool roomBooked = _dbContext.Bookings.Any
                 (b => b.RoomId == booking.RoomId &&
                 b.BookingId != booking.BookingId && //ignore current booking
                 booking.StartDate < b.EndDate &&
                 booking.EndDate > b.StartDate //checks for overlapping dates
                 );
            if (roomBooked)
            {
                throw new InvalidOperationException("This room is already booked for selected dates");
            }
            //update booking details
            existingBooking.StartDate= booking.StartDate;
            existingBooking.EndDate = booking.EndDate;
            existingBooking.NumberOfGuests = booking.NumberOfGuests;

            _dbContext.Bookings.Update(existingBooking);
            _dbContext.SaveChanges();

        }
    }


}
