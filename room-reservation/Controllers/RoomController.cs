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
        private readonly BuildingDomain _buildingDomain;
        private readonly FloorDomain _floorDomain;
        private readonly RoomTypeDomain _roomTypeDomain;
        public RoomController(RoomDomain roomDomain,BuildingDomain buildingDomain,FloorDomain floorDomain, RoomTypeDomain roomTypeDomain)
        {
            _roomDomain = roomDomain;
            _buildingDomain = buildingDomain;
            _floorDomain = floorDomain;
            _roomTypeDomain = roomTypeDomain;
        }

        public async Task<IActionResult> Index()
        {
                var rooms = await _roomDomain.GetAllRooms();
                return View(rooms);

        }

        [HttpGet]
        public async Task<IActionResult> AddRoom()
        {
            ViewBag.Building = new SelectList(await _buildingDomain.GetAllBuilding(),"Guid","BuildingNameAr");
            ViewBag.Floor = new SelectList(_roomDomain.GetAllFloors(), "Guid", "FloorNo");

            var roomTypes = await _roomTypeDomain.GetAllRoomTypes();
            var roomViewModel = new RoomViewModel
            {
                RoomTypes = roomTypes.Select( x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.RoomAR
                }).ToList()
            };

            return View(roomViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> AddRoom(RoomViewModel roomViewModel)
        {
            ViewBag.Building = new SelectList(await _buildingDomain.GetAllBuilding(), "Guid", "BuildingNameAr");
            ViewBag.Floor = new SelectList(_roomDomain.GetAllFloors(), "Guid", "FloorNo");

            try
            {
                if (ModelState.IsValid)
                {
                    await _roomDomain.InsertRoom(roomViewModel);
                    return Json(new { success = true, message = "Added successfully" });
                }
                else
                {
                    return Json(new { success = false, message = "Invalid data" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditRoom(int Id)
        {
            ViewBag.Building = new SelectList(await _buildingDomain.GetAllBuilding(), "Guid", "BuildingNameAr");
            ViewBag.Floor = new SelectList(_roomDomain.GetAllFloors(), "Guid", "FloorNo");

            var rooms = await _roomDomain.GetRoomById(Id);
            var roomTypes = await _roomTypeDomain.GetAllRoomTypes();
            var roomViewModel = new RoomViewModel
            {
                Id = rooms.Id,
                RoomTypes = roomTypes.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.RoomAR
                }).ToList()
            };

            return View(roomViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditRoom(RoomViewModel roomViewModel)
        {
            ViewBag.Building = new SelectList(await _buildingDomain.GetAllBuilding(), "Guid", "BuildingNameAr");
            ViewBag.Floor = new SelectList(_roomDomain.GetAllFloors(), "Guid", "FloorNo");

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _roomDomain.EditRoom(roomViewModel);
                    if (result)
                    {
                        return Json(new { success = true, message = "Updated successfully" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Update failed" });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }

            return Json(new { success = false, message = "Invalid data" });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            await _roomDomain.DeleteRoom(id);
            return Json(new { success = true });
        }


    }
}
