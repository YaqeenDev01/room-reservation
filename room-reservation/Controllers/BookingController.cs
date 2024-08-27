using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using room_reservation.Domain;
using room_reservation.Models;
using room_reservation.ViewModel;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace room_reservation.Controllers
{
    
    public class BookingController : Controller
    {

        private readonly BookingDomain _BookingDomain;
        private readonly FloorDomain _floorDomain;
        private readonly BuildingDomain _buildingDomain;
        private readonly RoomDomain _roomDomain;
        private readonly RoomTypeDomain _roomTypeDomain;
        
        
        public BookingController(BookingDomain bookingDomain,BuildingDomain buildingDomain, FloorDomain floorDomain, RoomDomain roomDomain ,RoomTypeDomain roomTypeDomain)
        {
            _BookingDomain = bookingDomain;
            _buildingDomain = buildingDomain;
            _floorDomain = floorDomain;
            _roomDomain = roomDomain;
            _roomTypeDomain = roomTypeDomain;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            
            
            // building name , floor no , capacity() 
                ViewBag.Building = new SelectList(await _buildingDomain.GetAllBuilding(),"Guid","BuildingNameAr");
                ViewBag.RoomTypes = new SelectList(await _roomTypeDomain.GetAllRoomTypes(), "guid", "RoomAR");
           
                return View();

           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Guid? buildingGuid, Guid? floorGuid,Guid? roomTypeGuid,int? seatCapacity)
        {
           // var rooms = await _roomDomain.GetAllRooms();
            // building name , floor no , capacity() 
            ViewBag.Building = new SelectList(await _buildingDomain.GetAllBuilding(),"Guid","BuildingNameAr",buildingGuid);
            ViewBag.RoomTypes = new SelectList(await _roomTypeDomain.GetAllRoomTypes(), "guid", "RoomAR",roomTypeGuid);
       
       
            var rooms = await _roomDomain.GetRoomByFilter(buildingGuid, floorGuid, roomTypeGuid, seatCapacity);
            return View(rooms);
            //
            // //map room view model to book view model
            // var bookings = rooms.Select(room => new BookingViewModel
            // {
            //     BuildingNameAr = room.Floor.Building.BuildingNameAr,
            //     FloorNo = room.Floor.FloorNo,
            //     RoomNo = room.RoomNo,
            //     RoomAR = room.RoomAR,
            //     SeatCapacity = room.SeatCapacity,
            //     guid = room.Guid
            //     
            // }).ToList();
            //
            // return View(bookings); 

        }
        
        // [HttpPost]
        // public async Task<IActionResult> Index(int? buildingId, int? floorId, int? roomTypeId, int? seatCapacity)
        // {
        //     // Populate dropdowns for filtering
        //     ViewBag.Building = new SelectList(await _buildingDomain.GetAllBuilding(), "Id", "BuildingNameAr");
        //
        //     ViewBag.RoomTypes = new SelectList(await _roomTypeDomain.GetAllRoomTypes(), "Id", "RoomAR");
        //
        //     var rooms = await _roomDomain.GetAllRooms();
        //
        //     // Apply filters based on selected values
        //     if (buildingId.HasValue)
        //     {
        //         rooms = rooms.Where(r => r.BuildingId == buildingId.Value);
        //     }
        //
        //     if (floorId.HasValue)
        //     {
        //         rooms = rooms.Where(r => r.FloorId == floorId.Value);
        //     }
        //
        //     if (roomTypeId.HasValue)
        //     {
        //         rooms = rooms.Where(r => r.RoomTypeId == roomTypeId.Value);
        //     }
        //
        //     if (seatCapacity.HasValue)
        //     {
        //         rooms = rooms.Where(r => r.SeatCapacity >= seatCapacity.Value);
        //     }
        //
        //     // Convert to ViewModel and pass to the view
        //     var roomViewModels = rooms.Select(r => new RoomViewModel
        //     {
        //         Guid = r.Guid,
        //         RoomNo = r.RoomNo,
        //         SeatCapacity = r.SeatCapacity,
        //         FloorId = r.FloorId,
        //         RoomTypeId = r.RoomTypeId,
        //         BuildingNameAr = r.BuildingNameAr,
        //         FloorNo = r.FloorNo,
        //         RoomAR = r.RoomAR
        //     }).ToList();
        //
        //     ViewBag.Rooms = roomViewModels;
        //     return View(roomViewModels);
        // }
        //
        // [HttpPost]
        // public async Task<IActionResult> Index()
        // {
        //     
        //     
        //     // building name , floor no , capacity() 
        //     ViewBag.Building = new SelectList(await _buildingDomain.GetAllBuilding(),"Guid","BuildingNameAr");
        //     ViewBag.RoomTypes = new SelectList(await _roomTypeDomain.GetAllRoomTypes(), "guid", "RoomAR");
        //     ViewBag.Room = await _roomDomain.GetAllRooms();
        //     //ViewBag.Rooms = rooms;
        //     return View();
        // }
        // // public async Task<IActionResult> Index(int? buildingId, int? floorId, int? roomTypeId, int? seatCapacity)
        // {
        //     // Call the FilterRooms to get filtered rooms
        //     var rooms = FilterRooms(buildingId, floorId, roomTypeId, seatCapacity);
        //
        //     
        //     ViewBag.Building = new SelectList(await _buildingDomain.GetAllBuilding(),"Guid","BuildingNameAr");
        //     ViewBag.RoomTypes = new SelectList(await _roomTypeDomain.GetAllRoomTypes(), "guid", "RoomAR");
        //     return View(rooms);
        // }

        // public IEnumerable<BookingViewModel>  FilterRooms(Guid? buildingId, Guid? floorId, Guid? roomTypeId, int? seatCapacity)
        // {
        //     var rooms = _BookingDomain.getAllBooking(); // Get all rooms initially
        //
        //     if (floorId.HasValue)
        //         rooms = rooms.Where(r => r.FloorId == floorId.Value);
        //
        //     if (roomTypeId.HasValue)
        //         rooms = rooms.Where(r => r.RoomTypeId == roomTypeId.Value);
        //
        //     if (seatCapacity.HasValue)
        //         rooms = rooms.Where(r => r.SeatCapacity >= seatCapacity.Value);
        //
        //     var roomList = rooms.ToList();
        //
        //     return roomList; // Returns partial view with filtered rooms
        // }

        
        
        public async Task<IList<FloorViewModel>> getFloorbyGuid(Guid id)
        {
            return await _floorDomain.GetFloorByBuildingGuid(id);
            
        }
   
        

        // view the bookings page 
        public async Task<IActionResult> Book()
        {
            var booking = await _BookingDomain.GetAllBooking();
            return View(booking);
        }
        

        [HttpGet]
        public async Task<IActionResult> Add(Guid id)
        {

          
            return View(  await _BookingDomain.getAllBookingByRoomGuid(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(BookingViewModel booking)
        {

            if (ModelState.IsValid)
            {
                _BookingDomain.AddBooking(booking);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(BookingViewModel booking)
        {
            if (ModelState.IsValid)
            {
                _BookingDomain.DeleteBooking(booking);
                return RedirectToAction(nameof(Index));
            }
            return View();

        }
        [HttpGet]
        public IActionResult Details(Guid id) {
            return View(_BookingDomain.getBookingByid(id));
        }

        [HttpPost]
        public IActionResult Details(BookingViewModel booking)
        {
            if (ModelState.IsValid)
            {
                
                _BookingDomain.DetailsBooking(booking);
                return RedirectToAction(nameof(Index));
            }
            return View();

        }

    }

}

 