using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using room_reservation.Domain;
using room_reservation.Models;
using room_reservation.ViewModel;

namespace room_reservation.Controllers
{
    public class BuildingController : Controller
    {

        private readonly BuildingDomain _BuildingDomain;
        public BuildingController(BuildingDomain buildingDomain)
        {
            _BuildingDomain = buildingDomain;

        }
        public async Task<IActionResult> Index( /*string successful,string Failed*/)
        {
            //ViewData["successful"] = successful;
            //ViewData["Failed"] = Failed;
            var buildings = await _BuildingDomain.GetAllBuilding();
            return View(buildings);
        }

        [HttpGet]
        public async Task<IActionResult> AddBuilding()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> AddBuilding(BuildingViewModel building)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _BuildingDomain.InsertBuilding(building);
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
        public async Task<IActionResult> EditBuilding(Guid id)
        {
            return View(_BuildingDomain.getBuildingByguid(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBuilding(BuildingViewModel building)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _BuildingDomain.UpdatBuilding(building);
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

        public async Task<IActionResult> Delet(Guid id)
        {
               await _BuildingDomain.DeleteBuilding(id);
               return Json(new { success = true });
       
        }
    }

//public IActionResult Delet(BuildingViewModel building)
//{
//    if (ModelState.IsValid)
//    {
//        _BuildingDomain.DeleteBuilding(building);
//        return RedirectToAction(nameof(Index));
//    }
//    return View();

//}


}