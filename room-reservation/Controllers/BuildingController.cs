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
                    int check = await _BuildingDomain.InsertBuilding(building);

                    if (check == 1)
                    {
                        return Json(new { success = true, message = "Added successfully" });
                    }
                    else if (check == 3)
                    {
                        return Json(new { success = false, message = "The code is exit " });
                    }
                    else if (check == 4)
                    {
                        return Json(new { success = false, message = "The building number is exit " });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Invalid data" });
                }
                return View();

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
            try
            {
                if (ModelState.IsValid)
                {
                    int check= await _BuildingDomain.UpdatBuilding(building);

                    if (check == 1)
                    {
                        return Json(new { success = true, message = "Added successfully" });
                    }
                    else if (check == 3)
                     {
                        return Json(new { success = false, message = "The code is exit " });
                      }
                    else if (check == 4)
                    { 
                        return Json(new { success = false, message = "The building number is exit " });
                     }

                }
                else
                {
                    return Json(new { success = false, message = "Invalid data" });
                }
                return View();
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        
        public async Task<IActionResult> Delet(Guid id)
        {
               await _BuildingDomain.DeleteBuilding(id);
               return Json(new { success = true });
       
        }
    }

}