using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using room_reservation.Domain;
using room_reservation.ViewModel;

namespace room_reservation.Controllers
{
    public class RoomController : Controller
    {
        private readonly RoomDomain _RoomDomain;

        public RoomController(RoomDomain RoomDomain)
        {
            _RoomDomain = RoomDomain;
        }

        public IActionResult Index()
        {
            try
            {
                var rooms = _RoomDomain.GetAllRooms();
                return View(rooms);
            }
            catch 
            {
                TempData["ErrorMessage"] = "An error occurred while retrieving the rooms. Please try again later.";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Floors = new SelectList(_RoomDomain.GetAllFloors(), "Id", "FloorNo");
            ViewBag.RoomTypes = new SelectList(_RoomDomain.GetAllRoomTypes(), "Id", "RoomAR");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RoomViewModel room)
        {
            if (ModelState.IsValid)
            {
                _RoomDomain.InsertRoom(room);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Floors = new SelectList(_RoomDomain.GetAllFloors(), "Id", "FloorNo");
            ViewBag.RoomTypes = new SelectList(_RoomDomain.GetAllRoomTypes(), "Id", "RoomAR");
            return View(room);
        }

        [HttpGet]
        public IActionResult Edit(Guid guid)
        {
            var room = _RoomDomain.GetRoomById(guid);
            if (room == null)
            {
                return NotFound();
            }
            ViewBag.Floors = new SelectList(_RoomDomain.GetAllFloors(), "Id", "FloorNo", room.FloorId);
            ViewBag.RoomTypes = new SelectList(_RoomDomain.GetAllRoomTypes(), "Id", "RoomAR", room.RoomTypeId);
            return View(room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RoomViewModel room)
        {
            if (ModelState.IsValid)
            {
                _RoomDomain.EditRoom(room);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Floors = new SelectList(_RoomDomain.GetAllFloors(), "Id", "FloorNo", room.FloorId);
            ViewBag.RoomTypes = new SelectList(_RoomDomain.GetAllRoomTypes(), "Id", "RoomAR", room.RoomTypeId);
            return View(room);
        }
    }
}