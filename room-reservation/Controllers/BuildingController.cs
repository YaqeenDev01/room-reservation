using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using room_reservation.Domain;
using room_reservation.Models;
using room_reservation.ViewModel;
using System.Security.Claims;

namespace room_reservation.Controllers
{
    //[Area("Admin")]

    public class BuildingController : Controller
    {

        private readonly BuildingDomain _BuildingDomain;
        public BuildingController(BuildingDomain buildingDomain)
        {
            _BuildingDomain = buildingDomain;
        }
        public async Task<IActionResult> Index()
        {
            var buildings = await _BuildingDomain.GetAllBuilding();
            return View(buildings);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> AddBuilding()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBuilding(BuildingViewModel building)
        {
            building.Email = User.FindFirst(ClaimTypes.Email).Value;


            if (!ModelState.IsValid)
            {
                // إذا كان هناك أخطاء في النموذج، إرجاع الأخطاء
                return Json(new { success = false, message = "Invalid data" });
            }

            try
            {
                int result = await _BuildingDomain.InsertBuilding(building);

                switch (result)
                {
                    case 1:
                        return Json(new { success = true, message = "Added successfully" });
                    case 3:
                        return Json(new { success = false, message = "The code already exists" });
                    case 4:
                        return Json(new { success = false, message = "The building number already exists" });
                    default:
                        return Json(new { success = false, message = "An unexpected error occurred" });
                }
            }
            catch (Exception ex)
            {
                // التعامل مع الاستثناءات وإرجاع رسالة الخطأ
                return Json(new { success = false, message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> EditBuilding(Guid id)
        {

            return View(  await _BuildingDomain.getBuildingByguid(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBuilding(BuildingViewModel building)
        {
            building.Email = User.FindFirst(ClaimTypes.Email).Value;


            if (!ModelState.IsValid)
            {
                // إذا كان هناك أخطاء في النموذج، إرجاع الأخطاء
                return Json(new { success = false, message = "Invalid data" });
            }

            try
            {
                int result = await _BuildingDomain.UpdatBuilding(building);

                switch (result)
                {
                    case 1:
                        return Json(new { success = true, message = " successfully" });
                    case 3:
                        return Json(new { success = false, message = "The code already exists" });
                    case 4:
                        return Json(new { success = false, message = "The building number already exists" });
                    default:
                        return Json(new { success = false, message = "An unexpected error occurred" });
                }
            }
            catch (Exception ex)
            {
                // التعامل مع الاستثناءات وإرجاع رسالة الخطأ
                return Json(new { success = false, message = ex.Message });
            }
        }
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> Delet(Guid id)

        {

            await _BuildingDomain.DeleteBuilding(id);
               return Json(new { success = true });
       
        }
    }

}