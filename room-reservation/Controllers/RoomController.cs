using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using room_reservation.Domain;
using room_reservation.ViewModel;


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
            ViewBag.Building = new SelectList(await _buildingDomain.GetAllBuilding(), "Guid", "BuildingNameAr");
            //ViewBag.Floor = new SelectList(await _floorDomain.GetAllFloors(), "Guid", "FloorNo");

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
           // ViewBag.Floor = new SelectList(await _floorDomain.GetAllFloors(), "Guid", "FloorNo");

            try
            {
                if (ModelState.IsValid)
                {
                    await _roomDomain.InsertRoom(roomViewModel);
                    TempData["SuccessMessage"] = "Added successfully";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(roomViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditRoom(int Id)
        {
            ViewBag.Building = new SelectList(await _buildingDomain.GetAllBuilding(), "Guid", "BuildingNameAr");
           // ViewBag.Floor = new SelectList(await _floorDomain.GetAllFloors(), "Guid", "FloorNo");

            var rooms =  _roomDomain.GetRoomById(Id);
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
       //     ViewBag.Floor = new SelectList(await _floorDomain.GetAllFloors(), "Guid", "FloorNo");

            try
            {
                if (ModelState.IsValid)
                {
                    await _roomDomain.EditRoom(roomViewModel);
                    TempData["SuccessMessage"] = "Added successfully";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(roomViewModel);

        }

        [HttpPost]
        public async Task<IActionResult> DeleteRoom(Guid guid)
        {
            await _roomDomain.DeleteRoom(guid);
            return Json(new { success = true });
        }


    }
}
