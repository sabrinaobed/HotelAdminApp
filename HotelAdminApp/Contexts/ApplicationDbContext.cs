using HotelAdminApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HotelAdminApp.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Booking> Bookings { get; set; } = null!;
        public DbSet<Invoice> Invoices { get; set; } = null!;


        //Parameterless Constructor (For EF Core CLI)
        public ApplicationDbContext() { }

        // Constructor for Dependency Injection (DI)
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        // Override OnConfiguring for EF Core CLI (Remove-Migration)
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) // Ensure we only set it if not already configured
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define One-to-Many relationship between Booking and Customer
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Customer)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Define One-to-Many relationship between Booking and Room
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Room)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            // Define One-to-One relationship between Booking and Invoice
            //Enable Cascade Delete from Booking → Invoice
            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Booking)
                .WithOne(b => b.Invoice)
                .HasForeignKey<Invoice>(i => i.BookingId)
                .OnDelete(DeleteBehavior.Cascade); //Now invoice gets deleted automatically when booking is deleted

            //Adding fluent API Constraints

            //Customer Constraints
            modelBuilder.Entity<Customer>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Customer>()
                .Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(255);

            //Room Constraints

            modelBuilder.Entity<Room>()
                .Property(r => r.RoomNumber)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<Room>()
                .Property(r => r.RoomType)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Room>()
                .Property(r => r.PricePerNight)
                .IsRequired()
                .HasColumnType("decimal(10,2)");


            // Invoice Constraints
            modelBuilder.Entity<Invoice>()
                .Property(i => i.TotalAmount)
                .IsRequired()
                .HasColumnType("decimal(10,2)");    



            base.OnModelCreating(modelBuilder);
        }
    }
}