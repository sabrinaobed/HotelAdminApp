using HotelAdminApp.Contexts;
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
    }
}
