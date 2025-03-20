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
        }
    }
}
