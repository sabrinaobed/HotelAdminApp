using HotelAdminApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAdminApp.Contexts
{
    class ApplicationDbContext : DbContext
    {
        //These DbSets represent the tables in the database 
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        //This is the default constructor wthout parameters and is used if you want to instanseria context without options
        public ApplicationDbContext()
        {

        }

        //This constructor accepts DbContextOptions and is to configure context in ASP.NET Core,it allows DI to provide database configuration
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }


        //This method configures the database connection settings, it is automatically executed when the context is used.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
         if(!optionsBuilder.IsConfigured)//checks if options are already configured
            {
                //Loads the configuration from the appsettings.json file
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                //Gets the connection string from the appsettings.json file
                var connectionString = configuration.GetConnectionString("DefaultConnection");

                //Configures the connection to the database SQL
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

    }



}
