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
                TempData["ErrorMessage"] = $"Error: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddRoom()
        {
            var floors = await _roomDomain.GetAllFloors();
            var roomTypes = await _roomDomain.GetAllRoomTypes();

            var roomViewModel = new RoomViewModel
            {
                Floors = floors.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.FloorNo.ToString()
                }).ToList(),

                RoomTypes = roomTypes.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.RoomAR
                }).ToList()
            };

            return View(roomViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRoom(RoomViewModel room)
        {
            if (ModelState.IsValid)
            {
                var result = await _roomDomain.InsertRoom(room);
                if (result == "1")
                {
                    TempData["SuccessMessage"] = "Room added successfully!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "An error occurred while saving the room.");
                }
            }

            var floors = await _roomDomain.GetAllFloors();
            var roomTypes = await _roomDomain.GetAllRoomTypes();

            room.Floors = floors.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.FloorNo.ToString()
            }).ToList();

            room.RoomTypes = roomTypes.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.RoomAR
            }).ToList();

            return View(room);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid guid)
        {
            var room = await _roomDomain.GetRoomByGuid(guid);
            if (room == null)
            {
                return NotFound();
            }

            room.Floors = (await _roomDomain.GetAllFloors()).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.FloorNo.ToString()
            }).ToList();

            room.RoomTypes = (await _roomDomain.GetAllRoomTypes()).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.RoomAR
            }).ToList();

            return View(room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoomViewModel room)
        {
            if (ModelState.IsValid)
            {
                var result = await _roomDomain.EditRoom(room);
                if (result == "1")
                {
                    TempData["SuccessMessage"] = "Room edited successfully!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "An error occurred while saving the room.");
                }
            }

            room.Floors = (await _roomDomain.GetAllFloors()).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.FloorNo.ToString()
            }).ToList();

            room.RoomTypes = (await _roomDomain.GetAllRoomTypes()).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.RoomAR
            }).ToList();

            return View(room);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid guid)
        {
            var room = await _roomDomain.GetRoomByGuid(guid);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid guid)
        {
            var result = await _roomDomain.DeleteRoom(guid);
            if (result == "1")
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
