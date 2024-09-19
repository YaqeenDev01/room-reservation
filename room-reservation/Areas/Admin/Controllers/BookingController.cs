using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using room_reservation.Domain;
using room_reservation.Models;
using room_reservation.ViewModel;
using System.Security.Claims;
namespace room_reservation.Areas.Admin.Controllers;
using OfficeOpenXml;
[Area("Admin")]
[Authorize(Roles = "Admin, SiteAdmin")]
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
    
        // User bookings
        [Authorize]
        public async Task<IActionResult>ViewBooking()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email).Value;
            var booking = await _BookingDomain.GetUserBookings(userEmail);
            return View(booking);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddBooking(Guid id)
        {
            return View(await _BookingDomain.getAllBookingByRoomGuid(id));
        }
        [HttpPost]
     
        [Authorize]
        public async Task<IActionResult> AddBooking(BookingViewModel booking)
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
     [Authorize]
        public async Task<IActionResult> Cancel(Guid id)
        {
            
                await _BookingDomain.CancelBooking(id,User.FindFirst(ClaimTypes.Email).Value);
                return Json(new { success = true });
                
        }
        [Authorize]
        [HttpGet]
        public IActionResult BookingDetails(Guid id) {
            return View(_BookingDomain.getBookingByid(id));
        }

        [HttpPost]
        public IActionResult BookingDetails(BookingViewModel booking)
        {
            if (ModelState.IsValid)
            {
                
                _BookingDomain.DetailsBooking(booking);
                return RedirectToAction(nameof(Index));
            }
            return View();

        }
 
  
 
    [Authorize(Roles = "Admin")]
    // All bookings shown to admin 
    // view the bookings page 
    public async Task<IActionResult> ViewAllBooking()
    {
        var booking = await _BookingDomain.GetAllBooking();
        return View(booking);
    }
    //All bookings of the same building shown to the site admin

    [Authorize(Roles = "Admin, SiteAdmin")]
    public async Task<IActionResult> ViewBuildingBookings()
    {
        var userEmail = User.FindFirst(ClaimTypes.Email).Value;
        var booking = await _BookingDomain.GetBuildingBookings(userEmail);
        return View(booking);
    }
    
  
    [Authorize(Roles = "Admin, SiteAdmin")]
    
    [HttpPost]
    public async Task<IActionResult> ApproveBooking(Guid id)
    {
       
            
        bool result = await _BookingDomain.ApproveBooking(id,User.FindFirst(ClaimTypes.Email)?.Value);
        if (result)
        {
            return Json(new { success = true });
        }
        return Json(new { success = false });
    }

    [Authorize(Roles = "Admin, SiteAdmin")]

    [HttpPost]
    public async Task<IActionResult> RejectBooking(Guid id, string rejectReason)
    {
        bool result = await _BookingDomain.RejectBooking(id, rejectReason,User.FindFirst(ClaimTypes.Email)?.Value);
        if (result)
        {
            return Json(new { success = true });
        }
        return Json(new { success = false });
    }

    [Authorize(Roles = "Admin, SiteAdmin")]
    public async Task<IActionResult> Orders()
    {
        var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
        var orders = await _BookingDomain.GetExtrnalBooking(userEmail);
        return View(orders);
    }
    

    [Authorize(Roles = "Admin, SiteAdmin")]
    [HttpGet]
    public async Task<IActionResult> OrderInfo(Guid id)
    {

        return View(await _BookingDomain.GetBookingByGuid(id));
    }
    [HttpPost]
    public async Task<IActionResult> OrderInfo( BookingViewModel booking)
    {
        if (ModelState.IsValid)
        {

            _BookingDomain.Orderinfo(booking);
            return View ( );
        }
        return View();


    }
    
    public async Task<ActionResult> ExportBooking()
    {
        var dataBookings = await _BookingDomain.GetExportableBookings();

        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Bookings");

            worksheet.Cells[1, 1].Value = "الاسم الكامل";
            worksheet.Cells[1, 2].Value = "البريد الإلكتروني";
            worksheet.Cells[1, 3].Value = "رقم الجوال";
            worksheet.Cells[1, 4].Value = "اسم مبنى المستخدم";
            worksheet.Cells[1, 5].Value = "اسم المبنى";
            worksheet.Cells[1, 6].Value = "رقم الطابق";
            worksheet.Cells[1, 7].Value = "رقم القاعة";
            worksheet.Cells[1, 8].Value = "نوع القاعة";
            worksheet.Cells[1, 9].Value = "تاريخ الحجز";
            worksheet.Cells[1, 10].Value = "بداية الحجز";
            worksheet.Cells[1, 11].Value = "نهاية الحجز";
            worksheet.Cells[1, 12].Value = "مدة الحجز";
            worksheet.Cells[1, 13].Value = "سبب الرفض";
            worksheet.Cells[1, 14].Value = "حالة الحجز";
         
            // Add data rows
            int row = 2;
            foreach (var Booking in dataBookings)
            {
                worksheet.Cells[row, 1].Value = Booking.FullName;
                worksheet.Cells[row, 2].Value = Booking.Email;
                worksheet.Cells[row, 3].Value = Booking.PhoneNumber;
                worksheet.Cells[row, 4].Value = Booking.UserBuildingAR;
                worksheet.Cells[row, 5].Value = Booking.BuildingNameAr;
                worksheet.Cells[row, 6].Value = Booking.FloorNo;
                worksheet.Cells[row, 7].Value = Booking.RoomNo;
                worksheet.Cells[row, 8].Value = Booking.RoomAR;
                worksheet.Cells[row, 9].Value = Booking.BookingDate;
                worksheet.Cells[row, 9].Style.Numberformat.Format = "yyyy-MM-dd";
                worksheet.Cells[row, 10].Value = Booking.BookingStart;
                worksheet.Cells[row, 10].Style.Numberformat.Format = "hh:mm";
                worksheet.Cells[row, 11].Value = Booking.BookingEnd;
                worksheet.Cells[row, 11].Style.Numberformat.Format = "hh:mm";
                worksheet.Cells[row, 12].Value = Booking.Duration;
                worksheet.Cells[row, 13].Value = Booking.RejectReason;
                worksheet.Cells[row, 14].Value = Booking.BookingStatusAR;

    
                row++;
            }
            worksheet.Column(4).Width = 15; // Adjust based on your content

            // Generate the file
            var stream = new MemoryStream();
            package.SaveAs(stream);
            stream.Position = 0;

            var fName = $"Bookings-{DateTime.Now:yyyyMMddHHmmss}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fName);
        }
    }

    
    
    
}
