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
                ViewBag.Floors = new SelectList(await _floorDomain.GetAllFloors(),"Guid","FloorNo");
                ViewBag.RoomTypes = new SelectList(await _roomTypeDomain.GetAllRoomTypes(), "guid", "RoomAR");
            

            return View();
        }
        [HttpPost]
        


        public async Task<IActionResult> Book()
        {
            var booking = await _BookingDomain.GetAllBooking(); 
            return View(booking);
            
            
        }
        
        
        public FloorViewModel getFloorbyGuid(Guid id)
        {
            return _floorDomain.GetFloorByBuildingGuid(id);
            
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

