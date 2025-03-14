using HotelAdminApp.Contexts;
using HotelAdminApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAdminApp.Data
{
    class DataInitializer
    {

        //This method creates and returns an instance of ApplicationDbContext
        public static ApplicationDbContext Build()
        {
            //loading configuration from appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build(); //Build the configuration object

            //Get the connection string from appsettings.json
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            //Create DbcOntextOptionsBuilder with SQL server
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                                 .UseSqlServer(connectionString) 
                                 .Options;

            //Create an instance of ApplicationDbContext using configured options
            var dbContext = new ApplicationDbContext(contextOptions);

            //applying migrations 
            dbContext.Database.Migrate();

            //retuen the configured DbContext instance
            return dbContext;
        }


        //These methods work together to check if database has existing data , if not then add data.

        //Checks if initial data exists in database and prevents adding again same data
        public static bool IfAnyDataExists(ApplicationDbContext dbContext)
        {
            return dbContext.Rooms.Any() || dbContext.Customers.Any() || dbContext.Customers.Any() || dbContext.Bookings.Any() || dbContext.Invoices.Any();
        }

        //Seed data
        public static void SeedData(ApplicationDbContext dbContext)
        {
            //check if data already exists
            if(!IfAnyDataExists(dbContext))
            {
                //add initial data if it doesnt exists
                SeedRooms(dbContext);
                SeedCustomers(dbContext);
                SeedBookings(dbContext);
                SeedInvoice(dbContext);
            }
        }


        //Seeding initial data for ROOMS
        public static void SeedRooms(ApplicationDbContext dbContext)
        {
            dbContext.Rooms.AddRange(new List<Room>
            {
                new Room { RoomNumber = "101",RoomType = "Single", Capacity = 1, PricePerNight = 100, ExtraBeds = 0},
                new Room { RoomNumber = "102",RoomType = "Double", Capacity = 2, PricePerNight = 150, ExtraBeds = 1},
                new Room { RoomNumber = "103",RoomType = "Double", Capacity = 3, PricePerNight = 200, ExtraBeds = 2},
                new Room { RoomNumber = "104",RoomType = "Single", Capacity = 1, PricePerNight = 100, ExtraBeds = 0},
            }
            );
            dbContext.SaveChanges();
        }

        //Seeding initial data for CUSTOMERS

        public static void SeedCustomers(ApplicationDbContext dbContext)
        {
            dbContext.Customers.AddRange(new List<Customer>
            {
            new Customer {Name = "Hannah Svensson", Email = "hannah@mail.com", PhoneNumber = "123456789" },
            new Customer {Name = "Sadrick John", Email = "sadrick@mail.com", PhoneNumber = "987654321" },
            new Customer {Name = "Sara Edward", Email = "sara@mail.com", PhoneNumber = "123498765" },
            new Customer {Name = "Bill Jacob", Email = "jacob@mail.com", PhoneNumber = "998877665" },
            });

            dbContext.SaveChanges();

        }

        //Seeding initial data for BOOKINGS
        public static void SeedBookings(ApplicationDbContext dbContext)
        {
            dbContext.Bookings.AddRange(new List<Booking>
            {
                new Booking{Room = dbContext.Rooms.FirstOrDefault(r => r.RoomNumber == "101"),Customer = dbContext.Customers.FirstOrDefault(c => c.Name == "Hannah Svensson"), StartDate = DateTime.Today.AddDays(1), EndDate = DateTime.Today.AddDays(3),NumberOfGuests = 1},
                new Booking{Room = dbContext.Rooms.FirstOrDefault(r => r.RoomNumber == "102"),Customer = dbContext.Customers.FirstOrDefault(c => c.Name == "Sadrick John "), StartDate = DateTime.Today.AddDays(5), EndDate = DateTime.Today.AddDays(7),NumberOfGuests = 2}
            });
            dbContext.SaveChanges();
        }

        //Seeding initial data for INVOICES
        public static void SeedInvoice(ApplicationDbContext dbContext)
        {
            dbContext.Invoices.AddRange(new List<Invoice>
            {
                new Invoice{Booking  = dbContext.Bookings.FirstOrDefault(b => b.Customer.Name == "Hannah Svensson"), TotalAmount = 200, IsPaid = false, DueDate = DateTime.Today.AddDays(10) },
                new Invoice{Booking  = dbContext.Bookings.FirstOrDefault(b => b.Customer.Name == "Sdrick John"), TotalAmount = 300, IsPaid = true, DueDate = DateTime.Today.AddDays(10) }
            });

            dbContext.SaveChanges();
        }


    }
}
