using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using room_reservation.Domain;
using room_reservation.Models;
using room_reservation.ViewModel;
namespace room_reservation.Controllers
{
    public class FloorController : Controller
    {
        private readonly FloorDomain _FloorDomain;
        private readonly BuildingDomain _BuildingDomain;


        public FloorController(FloorDomain FloorDomain, BuildingDomain BuildingDomain)
        {
           
            _FloorDomain = FloorDomain;
            _BuildingDomain = BuildingDomain;


        }
        //return all floors in the view
        public async Task<IActionResult> Index(string searchString)
        {
            var floors = await _FloorDomain.GetAllFloors();
            //activate quick search based on the building name 
            if (!String.IsNullOrEmpty(searchString))
            {
                floors = floors
                    .Where(f => f.BuildingNameAr.Contains(searchString))
                    .ToList();
            }

            return View(floors);
        }
       

        [HttpGet]
        public async Task<IActionResult> AddFloor()
        {
     
            // Create a SelectList from the buildingsName
            ViewBag.buildingsName = new SelectList( await _BuildingDomain.GetAllBuilding() ,"BuildingId","BuildingNameAr");

            // Create a SelectList from the buildingsName
            ViewBag.buildingsNo = new SelectList(await _BuildingDomain.GetAllBuilding() ,"BuildingId","BuildingNo");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFloor(FloorViewModel floor, int BuildingId)
        {
    
            ViewBag.buildingsName = new SelectList(await _BuildingDomain.GetAllBuilding(), "BuildingId", "BuildingNameAr");
            ViewBag.buildingsNo = new SelectList(await _BuildingDomain.GetAllBuilding(), "BuildingId", "BuildingNo");
            try
            {
                if (ModelState.IsValid)
                {
                    int check = await _FloorDomain.addFloor(floor);
                    if (check == 1)
                    {
                        return Json(new { success = true, message = "Added successfully" });

                    }
                    else
                    {
                        return Json(new { success = false, message = "Invalid data" });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Model state is invalid" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }

        [HttpGet]
        public async Task<IActionResult> EditFloor(Guid id)
        {
            //SelectList from the buildingsName
            ViewBag.buildingsName = new SelectList(await _BuildingDomain.GetAllBuilding() ,"BuildingId","BuildingNameAr");
            //  SelectList from the buildingsName
            ViewBag.buildingsNo = new SelectList(await _BuildingDomain.GetAllBuilding() ,"BuildingId","BuildingNo");
            return View(_FloorDomain.GetFloorByGuid(id));
        }
        
        [HttpPost]
        public async Task<IActionResult> EditFloor(FloorViewModel floor)
        {
            //SelectList from the buildingsName
            ViewBag.buildingsName = new SelectList(await _BuildingDomain.GetAllBuilding() ,"BuildingId","BuildingNameAr");
            
            //  SelectList from the buildingsName
            ViewBag.buildingsNo = new SelectList(await _BuildingDomain.GetAllBuilding() ,"BuildingId","BuildingNo");
            try
            {
                if (ModelState.IsValid)
                {
                    int check = await _FloorDomain.editFloor(floor);
                    if (check == 1)
                    {
                        return Json(new { success = true, message = "Added successfully" });

                    }
                    else
                    {
                        return Json(new { success = false, message = "Invalid data" });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Model state is invalid" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

          //  return View(floor);
        }
        public async Task<IList<FloorViewModel>> getFloorbyGuid(Guid id)
        {
            return await _FloorDomain.GetFloorByBuildingGuid(id);
            
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            await _FloorDomain.DeleteFloor(id);
            return Json(new { success = true });
        }
        
        
        //public async Task< IActionResult> Index()
        //{
        //    return View(await _FloorDomain.getAllFloors());
        //}
    }
}