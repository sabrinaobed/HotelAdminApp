using HotelAdminApp.Services;
using HotelAdminApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HotelAdminApp.Controllers
{
    public class RoomController //it handles user interactions for managing rooms and ensures data validation before callin RoomService
    {
        private readonly RoomService _roomService; //created its class variable

        //this constructor intializes room service via DI
        public RoomController(RoomService roomService)
        {
            _roomService = roomService;
        }


        //Get all Rooms

        public void GetAllRooms()
        {
            var rooms = _roomService.GetAllRooms();
            if (rooms.Count == 0)
            {
                Console.WriteLine("No rooms found");
            }
            else
            {
                Console.WriteLine("\n List of all Rooms: \n");
                foreach (var room in rooms)
                {
                    Console.WriteLine($"ID: {room.RoomId}, RoomNumber: {room.RoomNumber}, RoomType: {room.RoomType}, Capacity: {room.Capacity}, PricePerNight: {room.PricePerNight}");
                }
            }

            // Add this line to pause and let user see the output
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }





        //Get a room by Id
        public void GetRoomById(int id)
        {
            Console.Write("Enter Room ID: ");
            if (int.TryParse(Console.ReadLine(), out int roomId))
            {
                var room = _roomService.GetRoomById(roomId);
                if (room == null)
                {
                    Console.WriteLine($"ID: {room.RoomId}, RoomNumber: {room.RoomNumber}, RoomType: {room.RoomType},Capacity: {room.Capacity}, PricePerNight: {room.PricePerNight}");
                }
                else
                {
                    Console.WriteLine("Room not found");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID! Please enter correct ID and try again.");
            }

        }





        //Add a new room
        public void AddRoom()
        {
            Console.Clear();
            //Show the list before adding a new room
            Console.WriteLine("\nCurrent Room List:");
            var allRoomsListBeforeAddition = _roomService.GetAllRooms();
            foreach (var room in allRoomsListBeforeAddition)
            {
                Console.WriteLine($"ID: {room.RoomId}, RoomNumber: {room.RoomNumber}, RoomType: {room.RoomType}, Capacity: {room.Capacity}, PricePerNight: {room.PricePerNight}");
            }

            //ADD DETAILS FOR NEW ROOM
            Console.WriteLine("Enter Room Number: ");
            string roomNumber = Console.ReadLine();

            Console.WriteLine("Enter Room Type: ");
            string roomType = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(roomType))
            {
                Console.WriteLine("Room type cannot be empty");
                return;
            }


            Console.WriteLine("Enter Capacity: ");
            if (!int.TryParse(Console.ReadLine(), out int capacity) || capacity <= 0)
            {
                Console.WriteLine("Invalid capacity.");
                return;
            }


            Console.WriteLine("Enter Price Per night: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price <= 0)
            {
                Console.WriteLine("Invalid price.");
                return;
            }


            Console.WriteLine("Enter Extra Beds: ");
            if (!int.TryParse(Console.ReadLine(), out int extraBeds) || extraBeds < 0)
            {
                extraBeds = 0;
            }

            //adding room using room service
            Room newRoom = new Room
            {
                RoomNumber = roomNumber,
                RoomType = roomType,
                Capacity = capacity,
                PricePerNight = price,
                ExtraBeds = extraBeds
            };

            _roomService.AddRoom(newRoom);


            Console.WriteLine("\nRoom added successfully!");

            //Fetch all rooms again
            var allRooms = _roomService.GetAllRooms();

            // Show updated list
            Console.WriteLine("\nUpdated Room List:\n");

            foreach (var room in allRooms)
            {
                // If this is the newly added room, show it in green
                if (room.RoomNumber == newRoom.RoomNumber && room.RoomType == newRoom.RoomType && room.Capacity == newRoom.Capacity)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.WriteLine($"ID: {room.RoomId}, RoomNumber: {room.RoomNumber}, RoomType: {room.RoomType}, Capacity: {room.Capacity}, PricePerNight: {room.PricePerNight}");

                // Reset console color back to default
                Console.ResetColor();
            }

           

        }


        //Update a room

        public void UpdateRoom()
        {
            Console.WriteLine("Enter Room ID: ");
            if (int.TryParse(Console.ReadLine(), out int roomId))
            {
                var room = _roomService.GetRoomById(roomId);
                if (room == null)
                {
                    Console.WriteLine("Room not found");
                    return;
                }


                Console.WriteLine($"Updating Room {room.RoomNumber}\n");

                Console.Write("Enter new Room Type: ");
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                    room.RoomType = input;


                Console.Write("Enter new Capacity: ");
                if (int.TryParse(Console.ReadLine(), out int newCapacity) && newCapacity > 0)
                    room.Capacity = newCapacity;


                Console.Write("Enter new Price Per Night: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal newPrice) && newPrice > 0)
                    room.PricePerNight = newPrice;


                _roomService.UpdateRoom(room);
                Console.WriteLine("Room updated successfully!");

            }
            else
            {
                Console.WriteLine("Invalid input");
            }


        }




        //Delete a room

        public void DeleteRoom()
        {
            Console.WriteLine("Enter Room ID: ");
            if (int.TryParse(Console.ReadLine(), out int roomId))
            {
                var room = _roomService.GetRoomById(roomId);
                if (room == null)
                {
                    Console.WriteLine("Room not found");
                    return;
                }

                Console.Write($"Are you sure you want to delete Room {room.RoomNumber}? (yes/no: )");
                string confirmation = Console.ReadLine()?.ToLower();

                if (confirmation == "yes")
                {
                    _roomService.DeleteRoom(roomId);
                    Console.WriteLine("Room deleted successfully!");
                }
                else
                {
                    Console.WriteLine("Invalid Input");
                }
            }
        }
    }
}
