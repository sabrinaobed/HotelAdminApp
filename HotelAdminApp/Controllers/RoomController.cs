using HotelAdminApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAdminApp.Controllers
{
    public class RoomController //it handles user interactions for managing rooms and ensures data validation before callin RoomService
    {
        private readonly RoomService _roomService; //created its class variable

        //this constructor intializes room service via DI
        public RoomController (RoomService roomService)
        {
            _roomService = roomService;
        }


        //Get all Rooms
        public void GetAllRooms()
        {
            Console.Clear();
            var rooms = _roomService.GetAllRooms();
            if (rooms.Count == 0)
            {
                Console.WriteLine("No rooms found");
                return;
            }

            Console.WriteLine("\n List of Available Rooms: \n");
            foreach(var room in rooms)
            {
                Console.WriteLine($"ID: {room.RoomId}, RoomNumber: {room.RoomNumber}, RoomType: {room.RoomType},Capacity: {room.Capacity}, PricePerNight: {room.PricePerNight}");

            }

        }




        //Get a room by Id
        public void GetRoomById(int id)
        {
            Console.Write("Enter Room ID: ");
            if(int.TryParse(Console.ReadLine(), out int roomId))
            {
                var room = _roomService.GetRoomById(roomId);
                if(room == null)
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





    }
}
