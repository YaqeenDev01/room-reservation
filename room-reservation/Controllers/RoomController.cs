using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using room_reservation.Domain;
using room_reservation.ViewModel;
using System;

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
                TempData["ErrorMessage"] = "حصل خطأ. حاول مرة أخرى";
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
            ViewBag.Floors = new SelectList(_RoomDomain.GetAllFloors(), "Id", "FloorNo");
            ViewBag.RoomTypes = new SelectList(_RoomDomain.GetAllRoomTypes(), "Id", "RoomAR");

            if (ModelState.IsValid)
            {
                try
                {
                    if (bool.TryParse(room.IsActive.ToString(), out bool isActive))
                    {
                        room.IsActive = isActive;
                        _RoomDomain.InsertRoom(room);
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("IsActive", "من فضلك، أدخل قيمة صحيحة.");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($" {ex.Message}");
                }
            }

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
                if (bool.TryParse(room.IsActive.ToString(), out bool isActive))
                {
                    room.IsActive = isActive;
                    _RoomDomain.EditRoom(room);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("IsActive", "من فضلك، أضف قيمة صحيحة");
                }
            }

            ViewBag.Floors = new SelectList(_RoomDomain.GetAllFloors(), "Id", "FloorNo", room.FloorId);
            ViewBag.RoomTypes = new SelectList(_RoomDomain.GetAllRoomTypes(), "Id", "RoomAR", room.RoomTypeId);
            return View(room);
        }

        [HttpGet]
        public IActionResult Delete(Guid guid)
        {
            var room = _RoomDomain.GetRoomById(guid);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid guid)
        {
            _RoomDomain.DeleteRoom(guid);
            TempData["SuccessMessage"] = "تم الحذف بنجاح";
            return RedirectToAction(nameof(Index));
        }
    }
}
