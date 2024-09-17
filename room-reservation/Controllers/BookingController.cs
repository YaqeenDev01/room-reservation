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
        private readonly UserDomain _userDomain;
        
        public BookingController(BookingDomain bookingDomain,BuildingDomain buildingDomain, FloorDomain floorDomain, RoomDomain roomDomain ,RoomTypeDomain roomTypeDomain,UserDomain userDomain)
        {
            _BookingDomain = bookingDomain;
            _buildingDomain = buildingDomain;
            _floorDomain = floorDomain;
            _roomDomain = roomDomain;
            _roomTypeDomain = roomTypeDomain;
            _userDomain = userDomain;


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
        // All bookings shown to admin 
        // view the bookings page 
        // public async Task<IActionResult> Book()
        // {
        //     var booking = await _BookingDomain.GetAllBooking();
        //     return View(booking);
        // }
        
        // All bookings of the same building shown to the site admin
        public async Task<IActionResult> Book()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email).Value;
            var booking = await _BookingDomain.GetBuildingBookings(userEmail);
            return View(booking);
        }
        
        // User bookings
        // public async Task<IActionResult> Book()
        // {
        //     var userEmail = User.FindFirst(ClaimTypes.Email).Value;
        //     var booking = await _BookingDomain.GetUserBookings(userEmail);
        //     return View(booking);
        // }

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

  // Change to cancel and user booking statues to change it to cancel 
     
        public async Task<IActionResult> Cancel(Guid id)
        {
            
                await _BookingDomain.CancelBooking(id);
                return Json(new { success = true });
                
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
        [HttpPost]
        public async Task<IActionResult> ApproveBooking(Guid id)
        {
            bool result = await _BookingDomain.ApproveBooking(id);
            if (result)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> RejectBooking(Guid id, string rejectReason)
        {
            bool result = await _BookingDomain.RejectBooking(id, rejectReason);
            if (result)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }


        //[HttpPost]
        //public async Task<IActionResult> ApproveBooking(Guid id)
        //{
        //    bool result = await _BookingDomain.ApproveBooking(id);
        //               return RedirectToAction(nameof(Index));

        //}

        //[HttpPost]
        //public async Task<IActionResult> RejectBooking(Guid id, string rejectReason)
        //{
        //    bool result = await _BookingDomain.RejectBooking(id, rejectReason);

        //    return RedirectToAction(nameof(Index));


        //}

        public async Task<IActionResult> Orders()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var orders = await _BookingDomain.GetExtrnalBooking(userEmail);
            return View(orders);
        }
        [HttpGet]
        public async Task<IActionResult> Orderinfo(Guid id)
        {

             return View(await _BookingDomain.GetBookingByGuid(id));
        }
        [HttpPost]
        public async Task<IActionResult> Orderinfo( BookingViewModel booking)
        {
            if (ModelState.IsValid)
            {

                _BookingDomain.Orderinfo(booking);
                return View ( );
            }
            return View();


        }
       
    }
}
        


    




  