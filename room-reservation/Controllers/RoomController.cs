using Microsoft.AspNetCore.Mvc;
using room_reservation.Domain;
using room_reservation.Models;
using room_reservation.ViewModel;

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
            var rooms = await _roomDomain.GetAllRooms();
            return View(rooms);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var room = await _roomDomain.GetRoomById(id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        public IActionResult Create()
        {
          //  ViewBag.Floors = _roomDomain.GetAllFloors();
          //  ViewBag.RoomTypes = _roomDomain.GetAllRoomTypes();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomViewModel room)
        {
            if (ModelState.IsValid)
            {
                var result = await _roomDomain.InsertRoom(room);
                if (result == "added")
                {
                    TempData["SuccessMessage"] = "Room created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", $"An error occurred while saving the room: {result}");
                }
            }

          //  ViewBag.Floors = _roomDomain.GetAllFloors();
          //  ViewBag.RoomTypes = _roomDomain.GetAllRoomTypes();
            return View(room);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var room = await _roomDomain.GetRoomById(id);
            if (room == null)
            {
                return NotFound();
            }

            ViewBag.Floors = _roomDomain.GetAllFloors();
            ViewBag.RoomTypes = _roomDomain.GetAllRoomTypes();
            return View(room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RoomViewModel room)
        {
            if (ModelState.IsValid)
            {
                var result = _roomDomain.EditRoom(room);
                if (result == 1)
                {
                    TempData["SuccessMessage"] = "Room updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "An error occurred while updating the room.");
                }
            }

            ViewBag.Floors = _roomDomain.GetAllFloors();
            ViewBag.RoomTypes = _roomDomain.GetAllRoomTypes();
            return View(room);
        }

        public IActionResult Delete(Guid id)
        {
            var room = _roomDomain.GetRoomById(id).Result;
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var result = _roomDomain.DeleteRoom(id);
            if (result == 1)
            {
                TempData["SuccessMessage"] = "Room deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the room.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}