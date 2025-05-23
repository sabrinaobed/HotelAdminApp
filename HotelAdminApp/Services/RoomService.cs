﻿using HotelAdminApp.Contexts;
using HotelAdminApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAdminApp.Services
{
    public class RoomService
    {
        //showing reference to database context
        private readonly ApplicationDbContext _dbContext; //_dbContext is a class variable

        //This constructor intializes databse context via DI and ensure we can interact with database without re-intializing it.
        public RoomService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext; //_dbContext is a class variable ans dbContext is a parameter
        }

            //-------------------CURD OPERATIONS------------------------

                                //Get all Rooms
            public List<Room> GetAllRooms()
            {
            return _dbContext.Rooms.ToList();
            }





                             //Get a specific room by its ID
           public Room? GetRoomById(int id)
           {
            return _dbContext.Rooms.Find(id);//searches  a romm by its PK, if object found its fine other ,? represents its null otherwise.
           }





                             //Add a new room with all valid attributes
           public void AddRoom(Room room)
           {
           if(room == null)
           {
                throw new ArgumentNullException(nameof(room), "Room cannot be null.");
           }


          // Check for duplicate RoomNumber
            bool roomNumberExists = _dbContext.Rooms.Any(r => r.RoomNumber == room.RoomNumber);
           if (roomNumberExists)
                    throw new InvalidOperationException($"Room number '{room.RoomNumber}' already exists. Please enter a unique room number.");



            //validate room attributes
            if (string.IsNullOrWhiteSpace(room.RoomNumber))
           {
                throw new ArgumentException("Room number cannot be empty");
           }


           if(room.Capacity <= 0)
           {
           throw new ArgumentException("Room Capacity must be gretaer than 0");
           }


           if(room.PricePerNight <= 0)
           {
                throw new ArgumentException("Room price must be greater than 0");
           }

           
           _dbContext.Rooms.Add(room); //Add room to DbSet
           _dbContext.SaveChanges(); //Save changes to database
           }



        //Update a Room
        public void UpdateRoom(Room room)
        {
            if (room == null)
            {
                throw new ArgumentNullException(nameof(room), "Room cannot be null.");
            }
            //checking the existing room id details
            var existingRoom = _dbContext.Rooms.Find(room.RoomId);
            if (existingRoom == null)
            {
                throw new KeyNotFoundException($"Room with ID {room.RoomId} not found.");
            }
            //updating existing room details with new room details
            existingRoom.RoomNumber = room.RoomNumber;
            existingRoom.RoomType = room.RoomType;
            existingRoom.Capacity = room.Capacity;
            existingRoom.PricePerNight = room.PricePerNight;
            existingRoom.ExtraBeds = room.ExtraBeds;


            //marking room as updated
            _dbContext.Rooms.Update(existingRoom);
            _dbContext.SaveChanges();

        }
        //Delete a Room

        public void DeleteRoom(int id)
        {
            var room = _dbContext.Rooms.Find(id);
            if (room == null)
            {
                throw new KeyNotFoundException($"Room with ID {id} not found.");
            }

            // FIXED: only throw if hasBookings is true
            bool hasBookings = _dbContext.Bookings.AsNoTracking().Any(b => b.RoomId == id);
            if (hasBookings)
            {
                throw new InvalidOperationException("Cannot delete room with bookings.");
            }

            _dbContext.Rooms.Remove(room);
            _dbContext.SaveChanges();
        }

        //Searxh for Aviable rooms 
        public List<Room> SearchAvailableRooms(DateTime startDate, DateTime endDate, int numberOfGuests)
        {
            var bookedRooms = _dbContext.Bookings
                  .Where(b => startDate < b.EndDate && endDate > b.StartDate)
                  .Select(b => b.RoomId)
                  .Distinct()
                  .ToList();

            return _dbContext.Rooms
                .Where(r => !bookedRooms.Contains(r.RoomId) && r.Capacity >= numberOfGuests)
                .ToList();


        }

    }
}

