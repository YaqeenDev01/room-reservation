using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using room_reservation.Domain;
using room_reservation.ViewModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace room_reservation.Controllers
{
    public class RoomController : Controller
    {
        private readonly RoomDomain _roomDomain;

        public RoomController(RoomDomain roomDomain)
        {
            _roomDomain = roomDomain;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var rooms = await _roomDomain.GetAllRooms();
                return View(rooms);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"حصل خطأ: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult AddRoom()
        {

            ViewBag.FloorBag = new SelectList(_roomDomain.GetAllFloors(), "Id", "FloorNo");
            ViewBag.RoomTypeBag = new SelectList(_roomDomain.GetAllRoomTypes(), "Id", "RoomAR");

            return View();
        }

        [HttpPost]
        public IActionResult AddRoom(RoomViewModel room)
        {
            ViewBag.FloorBag = new SelectList(_roomDomain.GetAllFloors(), "Id", "FloorNo");
            ViewBag.RoomTypeBag = new SelectList(_roomDomain.GetAllRoomTypes(), "Id", "RoomAR");

            if (ModelState.IsValid)
            {
                string result =  _roomDomain.InsertRoom(room);
                if (result == "1")
                {
                    ViewData["Successful"] = "تمت العملية بنجاح";
                }
                else
                {
                    ViewData["Failed"] = result;
                }
            }
            return View(room);

        }

        [HttpGet]
        public IActionResult EditRoom(Guid guid)
        {
            ViewBag.FloorBag = new SelectList(_roomDomain.GetAllFloors(), "Id", "FloorNo");
            ViewBag.RoomTypeBag = new SelectList(_roomDomain.GetAllRoomTypes(), "Id", "RoomAR");

            return View(_roomDomain.GetRoomByGuid(guid));
        }

        [HttpPost]
        public IActionResult EditRoom(RoomViewModel room)
        {
            ViewBag.FloorBag = new SelectList(_roomDomain.GetAllFloors(), "Id", "FloorNo");
            ViewBag.RoomTypeBag = new SelectList(_roomDomain.GetAllRoomTypes(), "Id", "RoomAR");

            if (ModelState.IsValid)
            {
                string result = _roomDomain.EditRoom(room);
                if (result == "1")
                {
                    ViewData["Successful"] = "تمت العملية بنجاح";
                }
                else
                {
                    ViewData["Failed"] = result;
                }
                _roomDomain.EditRoom(room);
            }
            return View(room);

        }

        public IActionResult DeleteRoom(Guid guid)
        {
            string check = _roomDomain.DeleteRoom(guid);
            if (check == "1")

                ViewData["Successful"] = "تم الحذف بنجاح";

            else
                ViewData["Failed"] = check;

            _roomDomain.DeleteRoom(guid);
            return View();
        }


    }
}
