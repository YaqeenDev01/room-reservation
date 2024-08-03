using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using room_reservation.Domain;
using room_reservation.ViewModel;

namespace room_reservation.Controllers
{
    public class _RoomController : Controller
    {
        private readonly RoomDomain _RoomDomain;

        public _RoomController(RoomDomain RoomDomain)
        {
            _RoomDomain = RoomDomain;
        }
        public IActionResult Index()
        {
            var rooms = _RoomDomain.getAllRooms();
            return View(rooms);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Floors = new SelectList(_RoomDomain.getAllFloors(), "Id", "FloorNo");
            ViewBag.RoomTypes = new SelectList(_RoomDomain.getAllRoomTypes(), "Id", "RoomAR");
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
            ViewBag.Floors = new SelectList(_RoomDomain.getAllFloors(), "Id", "FloorNo");
            ViewBag.RoomTypes = new SelectList(_RoomDomain.getAllRoomTypes(), "Id", "RoomAR");
            return View(room);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var room = _RoomDomain.GetRoomById(id);
            if (room == null)
            {
                return NotFound();
            }
            ViewBag.Floors = new SelectList(_RoomDomain.getAllFloors(), "Id", "FloorNo", room.FloorId);
            ViewBag.RoomTypes = new SelectList(_RoomDomain.getAllRoomTypes(), "Id", "RoomAR", room.RoomTypeId);
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
            ViewBag.Floors = new SelectList(_RoomDomain.getAllFloors(), "Id", "FloorNo", room.FloorId);
            ViewBag.RoomTypes = new SelectList(_RoomDomain.getAllRoomTypes(), "Id", "RoomAR", room.RoomTypeId);
            return View(room);
        }
    }
}
