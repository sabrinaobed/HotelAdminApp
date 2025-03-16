using HotelAdminApp.Contexts;
using HotelAdminApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAdminApp.Data
{
    public class DataInitializer
    {

      //Mthod to build DbContext with configuration
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
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                 .UseSqlServer(connectionString) 
                                 .Options;

            //Create an instance of ApplicationDbContext using configured options
            var dbContext = new ApplicationDbContext(options);

            //applying migrations 
            dbContext.Database.Migrate();

            //Seed Data if required
            SeedData(dbContext);

           

            //retuen the configured DbContext instance
            return dbContext;
        }


        //These methods work together to check if database has existing data , if not then add data.

        //Checks if initial data exists in database and prevents adding again same data
        public static bool IfAnyDataExists(ApplicationDbContext dbContext)
        {
            return dbContext.Rooms.Any() || dbContext.Customers.Any() || dbContext.Bookings.Any() || dbContext.Invoices.Any();
        }

        //Seed intial data
        public static void SeedData(ApplicationDbContext dbContext)
        {
            Console.WriteLine("Checking if database has data...");

            if (!IfAnyDataExists(dbContext)) // Check if data exists
            {
                Console.WriteLine("Seeding data now...");

                SeedRooms(dbContext);
                SeedCustomers(dbContext);
                SeedBookings(dbContext);
                SeedInvoices(dbContext);

                Console.WriteLine("Seeding completed!");
            }
            else
            {
                Console.WriteLine("Data already exists. Skipping seeding.");
            }
        }



        //Seeding initial data for ROOMS
        public static void SeedRooms(ApplicationDbContext dbContext)
        {
            dbContext.Rooms.AddRange(new List<Room>
            {
                new Room { RoomNumber = "101",RoomType = "Single", Capacity = 1, PricePerNight = 100m, ExtraBeds = 0},
                new Room { RoomNumber = "102",RoomType = "Double", Capacity = 2, PricePerNight = 150m, ExtraBeds = 1},
                new Room { RoomNumber = "103",RoomType = "Suite", Capacity = 3, PricePerNight = 200m, ExtraBeds = 2},
                new Room { RoomNumber = "104",RoomType = "Single", Capacity = 1, PricePerNight = 100m, ExtraBeds = 0},
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
            var room101 = dbContext.Rooms.First(r => r.RoomNumber == "101").RoomId;
            var room102 = dbContext.Rooms.First(r => r.RoomNumber == "102").RoomId;
            var customer1 = dbContext.Customers.First(c => c.Name == "Hannah Svensson").CustomerId;
            var customer2 = dbContext.Customers.First(c => c.Name == "Sadrick John").CustomerId;

            dbContext.Bookings.AddRange(new List<Booking>
        {
            new Booking
            {
                RoomId = room101,
                CustomerId = customer1,
                StartDate = DateTime.Today.AddDays(1),
                EndDate = DateTime.Today.AddDays(3),
                NumberOfGuests = 1
            },
            new Booking
            {
                RoomId = room102,
                CustomerId = customer2,
                StartDate = DateTime.Today.AddDays(5),
                EndDate = DateTime.Today.AddDays(7),
                NumberOfGuests = 2
            }
        });
            dbContext.SaveChanges();
        }

        //Seeding initial data for INVOICES
        public static void SeedInvoices(ApplicationDbContext dbContext)
        {
            var bookings = dbContext.Bookings.ToList();

            dbContext.Invoices.AddRange(new List<Invoice>
        {
            new Invoice { BookingId = bookings[0].BookingId, TotalAmount = 200m, IsPaid = false, DueDate = DateTime.Today.AddDays(10) },
            new Invoice { BookingId = bookings[1].BookingId, TotalAmount = 300m, IsPaid = true, DueDate = DateTime.Today.AddDays(10) }
        });
            dbContext.SaveChanges();
        }


    }
}

