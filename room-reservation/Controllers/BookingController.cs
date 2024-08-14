using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Index(Guid FloorId)
        {
            
            
            // building name , floor no , capacity() 
            ViewBag.Building = new SelectList(await _buildingDomain.GetAllBuilding(),"Guid","BuildingNameAr");
            ViewBag.RoomTypes = new SelectList(await _roomTypeDomain.GetAllRoomTypes(), "guid", "RoomAR");
            ViewBag.Room = await _roomDomain.GetAllRooms();

            
            
            

            return View();
        }
        // public async Task<IActionResult> Index(int? buildingId, int? floorId, int? roomTypeId, int? seatCapacity)
        // {
        //     // Call the FilterRooms to get filtered rooms
        //     var rooms = FilterRooms(buildingId, floorId, roomTypeId, seatCapacity);
        //
        //     
        //     ViewBag.Building = new SelectList(await _buildingDomain.GetAllBuilding(),"Guid","BuildingNameAr");
        //     ViewBag.RoomTypes = new SelectList(await _roomTypeDomain.GetAllRoomTypes(), "guid", "RoomAR");
        //     return View(rooms);
        // }

        // public IEnumerable<RoomViewModel>  FilterRooms(int? buildingId, int? floorId, int? roomTypeId, int? seatCapacity)
        // {
        //     var rooms = _roomDomain.GetAllRooms(); // Get all rooms initially
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

        public async Task<IActionResult> Book()
        {
            var booking = await _BookingDomain.GetAllBooking(); 
            return View(booking);
            
            
        }
        
        
        public async Task<IList<FloorViewModel>> getFloorbyGuid(Guid id)
        {
            return await _floorDomain.GetFloorByBuildingGuid(id);
            
        }
   
        

        // view the bookings page 
        public async Task<IActionResult> Details()
        {
            var booking = await _BookingDomain.GetAllBooking();
            return View(booking);
        }
        

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(BookingViewModel booking)
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

