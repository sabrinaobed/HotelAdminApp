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
    
    
    
    
    
    }


}
