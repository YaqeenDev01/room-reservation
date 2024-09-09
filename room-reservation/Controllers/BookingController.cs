using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using room_reservation.Domain;
using room_reservation.Models;
using room_reservation.ViewModel;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> Index(Guid? buildingGuid, Guid? floorGuid,Guid? roomTypeGuid,int? seatCapacity)
        {
           // var rooms = await _roomDomain.GetAllRooms();
            // building name , floor no , capacity() 
            ViewBag.Building = new SelectList(await _buildingDomain.GetAllBuilding(),"Guid","BuildingNameAr",buildingGuid);
            ViewBag.RoomTypes = new SelectList(await _roomTypeDomain.GetAllRoomTypes(), "guid", "RoomAR",roomTypeGuid);
       
       
            var rooms = await _roomDomain.GetRoomByFilter(buildingGuid, floorGuid, roomTypeGuid, seatCapacity);
            return View(rooms);
 

        }
        
        
        
        public async Task<IList<FloorViewModel>> getFloorbyGuid(Guid id)
        {
            return await _floorDomain.GetFloorByBuildingGuid(id);
            
        }

        public async Task<IList<FloorViewModel>> getFloorbyId(int id)
        {
            return await _floorDomain.GetFloorByBuildingId(id);

        }

        // view the bookings page 
        public async Task<IActionResult> Book()
        {
            var booking = await _BookingDomain.GetAllBooking();
            return View(booking);
        }
        
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Add(Guid id)
        {
            return View(await _BookingDomain.getAllBookingByRoomGuid(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Add(BookingViewModel booking)
        {
          
         
                booking.Email =User.FindFirst(ClaimTypes.Email).Value;
                try
                {
                    if (ModelState.IsValid)
                    {
                        int check =  await _BookingDomain.AddBooking(booking);
                        if (check == 1)
                        {
                            return Json(new { success = true, message = "تم الحجز بنجاح" });

                        }
                        else
                        {
                            return Json(new { success = false, message = "لم يتم الحجز" });
                        }
                    }
                    else
                    {
                        return Json(new { success = false, message = "لابد من إدخال بيانات الحجز" });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            
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

 